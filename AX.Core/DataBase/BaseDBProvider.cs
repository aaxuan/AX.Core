using AX.Core.CommonModel;
using AX.Core.DataBase.Configs;
using AX.Core.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AX.Core.DataBase
{
    public abstract class BaseDBProvider
    {
        public BaseDBProvider()
        {
            _sqlBuilder = new SqlBuilder(LeftEscapeChar, RightEscapeChar, DbParmChar);
        }

        public void UseConfig(IDBConfig dBConfig)
        {
            _dbConfig = dBConfig;
            _sqlBuilder = new SqlBuilder(dBConfig.LeftEscapeChar, dBConfig.RightEscapeChar, dBConfig.DbParmChar);
        }

        #region 私有变量/方法

        public IDBConfig _dbConfig;

        public DbTransaction _dbTransaction;

        public SqlBuilder _sqlBuilder;

        public void TryOpenConn()
        {
            if (Connection.State != ConnectionState.Open)
            { Connection.Open(); }
        }

        public void SetParameters(DbCommand cmd, string sql, object[] args)
        {
            var regexStr = DbParmChar + "\\w+";
            var parmars = Regex.Matches(sql, regexStr);

            //无参数情况
            if (parmars.Count <= 0)
            { return; }

            //多参数一对象情况
            if (parmars.Count > 1 && args.Length == 1)
            {
                var obj = args.First();
                var objType = obj.GetType();

                for (int i = 0; i < parmars.Count; i++)
                {
                    Match parm = parmars[i];
                    cmd.Parameters.Add(_sqlBuilder.GetParameter(cmd, obj, objType.GetProperty(parm.Value.Substring(1, parm.Value.Length - 1))));
                }
            }

            if (parmars.Count != args.Length)
            {
                throw new AXDataBaseException($"参数数量错误 请检查SQL语句【{sql}】 参数【{string.Join(",", args)}】", sql);
            }

            //一一对应
            for (int i = 0; i < parmars.Count; i++)
            {
                Match parm = parmars[i];
                DbParameter par = Factory.CreateParameter();
                par.ParameterName = parm.Value.Substring(1, parm.Value.Length - 1);
                par.Value = args[i];
                par.DbType = _sqlBuilder.GetDbType(args[i]);
                cmd.Parameters.Add(par);
            }
        }

        private void CreateSqlCommand(DbCommand cmd, CommandType cmdType, string sql, object[] args)
        {
            TryOpenConn();
            cmd.Connection = Connection;
            cmd.CommandText = sql;
            cmd.CommandType = cmdType;
            //事务
            if (_dbTransaction != null)
            { cmd.Transaction = _dbTransaction; }
            //参数
            if (args != null && args.Length != 0)
            {
                SetParameters(cmd, sql, args);
            }
        }

        #endregion 私有变量/方法

        #region 可以子类实现 的属性/方法

        /// <summary>
        /// sql语句中为屏蔽关键字使用的左侧关键字特殊字符
        /// </summary>
        public abstract string LeftEscapeChar { get; }

        /// <summary>
        /// sql语句中为屏蔽关键字使用的右侧关键字特殊字符
        /// </summary>
        public abstract string RightEscapeChar { get; }

        /// <summary>
        /// 传参使用的参数符号
        /// </summary>
        public abstract string DbParmChar { get; }

        /// <summary>
        /// 获取链接
        /// </summary>
        public abstract DbConnection Connection { get; }

        /// <summary>
        ///
        /// </summary>
        public abstract DbProviderFactory Factory { get; }

        /// <summary>
        /// 获取链接
        /// </summary>
        public virtual int Timeout { get { return 50000; } }

        #endregion 可以子类实现 的属性/方法

        #region Execute

        public int ExecuteNonQuery(string sql, params object[] args)
        {
            TryOpenConn();
            var cmd = Connection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            return cmd.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>(string sql, params object[] args)
        {
            TryOpenConn();
            var cmd = Connection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            var result = cmd.ExecuteScalar();
            return (T)Convert.ChangeType(result, typeof(T));
        }

        #endregion Execute

        //public void Save<T>(T entity)
        //{
        //    if (IsExists<T>(entity))
        //    { Update<T>(entity); }
        //    else
        //    { Insert<T>(entity); }
        //}

        #region 插入

        public T Insert<T>(T entity)
        {
            if (entity == null)
            { throw new AXDataBaseException($"不能保存 Null 实体 【{typeof(T).FullName}】", string.Empty); }
            var sql = _sqlBuilder.BuildInsertSql<T>(SchemaManage.GetTableName<T>());
            var result = ExecuteNonQuery(sql.ToString(), entity);
            return entity;
        }

        #endregion 插入

        #region 更新

        public int Update<T>(T entity)
        {
            var key = SchemaManage.GetPrimaryKey<T>();
            if (entity == null)
            { throw new AXDataBaseException($"不能更新 Null 实体 【{typeof(T).FullName}】", string.Empty); }
            var sb = _sqlBuilder.BuildUpdateByIdSql<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties().Where(p => p.Name != key.Name).Select(p => p.Name).ToArray());
            var result = ExecuteNonQuery(sb.ToString(), entity);
            return result;
        }

        #endregion 更新

        #region 删除

        public int Delete<T>(T entity)
        {
            if (entity == null)
            { return 0; }
            var idvalue = SchemaManage.GetPrimaryKey<T>().GetValue(entity);
            return Delete<T>(idvalue);
        }

        public int Delete<T>(string sql, params object[] args)
        {
            var sb = new StringBuilder();
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { sb = _sqlBuilder.BuildDeleteTableSqlNoWhere(SchemaManage.GetTableName<T>()); }
            var result = ExecuteNonQuery(sb.Append(sql).ToString(), args);
            return result;
        }

        public int Delete<T>(object PrimaryKey)
        {
            if (PrimaryKey == null)
            { return 0; }
            var sb = _sqlBuilder.BuildDeleteByIdSql<T>(SchemaManage.GetTableName<T>());
            var result = ExecuteNonQuery(sb.ToString(), PrimaryKey);
            return result;
        }

        #endregion 删除

        #region 查询

        //public bool IsExists<T>(T entity)
        //{
        //
        //    if (entity == null)
        //    { return false; }
        //    var tableMetaData = TableMapper.GetMetaData(currenttype);
        //    var idvalue = tableMetaData.IdProperty.GetValue(entity);
        //    return IsExists<T>(idvalue);
        //}

        //public bool IsExists<T>(object id)
        //{
        //
        //    if (id == null)
        //    { return false; }
        //    var tableMetaData = TableMapper.GetMetaData(currenttype);
        //    var sb = _common.BuildSelectCountSql(GetSchemaTableName(currenttype));
        //    sb.AppendFormat(" AND {0} ", _common.GetEqualConditions(tableMetaData.IdProperty.Name).First());
        //    var result = Connection.ExecuteScalar<int>(sb.ToString(), _common.GetDynamicParameters(sb.ToString(), new object[] { id }), DbTransaction, CommandTimeout);
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public long GetCount<T>()
        {
            var sb = _sqlBuilder.BuildSelectCountSql(SchemaManage.GetTableName<T>());
            var result = ExecuteScalar<long>(sb.ToString(), null);
            return result;
        }

        public long GetCount<T>(string sql, params object[] args)
        {
            var sb = new StringBuilder();
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { sb = _sqlBuilder.BuildSelectCountSqlNoWhere(SchemaManage.GetTableName<T>()); }
            var result = ExecuteScalar<long>(sb.Append(sql).ToString(), args);
            return result;
        }

        public DataTable GetDataTable(string sql, params object[] args)
        {
            var result = new DataTable();
            var cmd = Connection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            var reader = cmd.ExecuteReader();
            result.Load(reader);
            return result;
        }

        public List<T> GetList<T>()
        {
            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties());
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>();
        }

        public List<T> GetList<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>();
        }

        public T SingleOrDefault<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>().SingleOrDefault();
        }

        public T SingleOrDefaultById<T>(object id)
        {
            if (id == null)
            { return default(T); }

            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties());
            var parmName = _sqlBuilder.UseParmChar(SchemaManage.GetPrimaryKey<T>().Name);
            sb.AppendFormat(" AND {0} = {1}", SchemaManage.GetPrimaryKey<T>().Name, parmName);
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>().SingleOrDefault();
        }

        public T FirstOrDefault<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>().FirstOrDefault();
        }

        public T FirstOrDefaultById<T>(object id)
        {
            if (id == null)
            { return default(T); }

            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaManage.GetTableName<T>(), typeof(T).GetProperties());
            var parmName = _sqlBuilder.UseParmChar(SchemaManage.GetPrimaryKey<T>().Name);
            sb.AppendFormat(" AND {0} = {1}", SchemaManage.GetPrimaryKey<T>().Name, parmName);
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>().FirstOrDefault();
        }

        #endregion 查询

        #region 事务

        public void BeginTransaction()
        {
            if (_dbTransaction != null)
            { throw new AXDataBaseException($"已存在事务", string.Empty); }
            _dbTransaction = Connection.BeginTransaction();
        }

        public void CompleteTransaction()
        {
            if (_dbTransaction == null)
            { throw new AXDataBaseException($"当前无事务", string.Empty); }
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public void AbortTransaction()
        {
            if (_dbTransaction == null)
            { throw new AXDataBaseException($"当前无事务", string.Empty); }
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        #endregion 事务

        #region 结构

        //public abstract string SetSchema<T>(bool execute);

        //public abstract string SetSchema(Type type, bool execute);

        #endregion 结构

        public void Dispose()
        {
            if (Connection != null)
            {
                this.Connection.Close();
                this.Connection.Dispose();
            }
        }
    }
}