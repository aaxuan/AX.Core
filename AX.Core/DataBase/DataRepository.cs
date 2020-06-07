using AX.Core.CommonModel.Exceptions;
using AX.Core.DataBase.Configs;
using AX.Core.DataBase.Schema;
using AX.Core.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static AX.Core.DataBase.DBFactory;

namespace AX.Core.DataBase
{
    public class DataRepository
    {
        public DataRepository(DbConnection dbConnection)
        {
            this._dbConnection = dbConnection;
        }

        public DataRepository UseConfig(DataBaseType dataBaseType)
        {
            switch (dataBaseType)
            {
                case DataBaseType.None: return this;
                case DataBaseType.MySql: return this.UseConfig(new MySqlDialectConfig());
                default: return this;
            }
        }

        public DataRepository UseConfig(IDBDialectConfig dBConfig)
        {
            this._dbConfig = dBConfig;
            this._sqlBuilder = new SqlBuilder(dBConfig.LeftEscapeChar, dBConfig.RightEscapeChar, dBConfig.DbParmChar);
            return this;
        }

        #region 私有变量/方法

        private IDBDialectConfig _dbConfig;

        private DbTransaction _dbTransaction;

        private SqlBuilder _sqlBuilder;

        private DbConnection _dbConnection;

        private void TryOpenConn()
        {
            if (Connection == null)
            { throw new AXDataBaseException("链接对象为空"); }

            if (Connection.State != ConnectionState.Open)
            { Connection.Open(); }
        }

        private void SetParameters(DbCommand cmd, string sql, object[] args)
        {
            var regexStr = _dbConfig.DbParmChar + "\\w+";
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
                return;
            }

            if (parmars.Count != args.Length)
            {
                throw new AXDataBaseException($"参数数量错误 请检查SQL语句【{sql}】 参数【{string.Join(",", args)}】", sql);
            }

            //一一对应
            for (int i = 0; i < parmars.Count; i++)
            {
                Match parm = parmars[i];
                DbParameter par = cmd.CreateParameter();
                par.ParameterName = parm.Value.Substring(1, parm.Value.Length - 1);
                par.Value = args[i];
                par.DbType = _sqlBuilder.GetDbType(args[i]);
                cmd.Parameters.Add(par);
            }
        }

        private void CreateSqlCommand(DbCommand cmd, CommandType cmdType, string sql, object[] args)
        {
            cmd.Connection = Connection;
            cmd.CommandText = sql;
            cmd.CommandType = cmdType;
            //事务
            if (_dbTransaction != null)
            { cmd.Transaction = _dbTransaction; }
            //参数
            if (args != null && args.Length != 0)
            { SetParameters(cmd, sql, args); }

            TryOpenConn();
        }

        #endregion 私有变量/方法

        #region 可继承实现 的属性/方法

        public virtual string NewGuId { get { return Guid.NewGuid().ToString("N"); } }

        /// <summary>
        /// 获取链接
        /// 继承类应实现私有变量缓存链接
        /// </summary>
        public virtual DbConnection Connection { get { return _dbConnection; } }

        public virtual int Timeout { get { return 50000; } }

        #endregion 可继承实现 的属性/方法

        #region Execute

        public int ExecuteNonQuery(string sql, params object[] args)
        {
            var cmd = _dbConnection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            return cmd.ExecuteNonQuery();
        }

        public T ExecuteScalar<T>(string sql, params object[] args)
        {
            var cmd = _dbConnection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            object result = cmd.ExecuteScalar();
            return (T)Convert.ChangeType(result, typeof(T));
        }

        #endregion Execute

        public void Save<T>(T entity)
        {
            if (IsExists<T>(SchemaProvider.GetPrimaryKey<T>().GetValue(entity, null)))
            { Update<T>(entity); }
            else
            { Insert<T>(entity); }
        }

        #region 插入

        public T Insert<T>(T entity)
        {
            if (entity == null)
            { throw new AXDataBaseException($"不能保存 Null 实体 【{typeof(T).FullName}】"); }
            //自动设置主键
            var key = SchemaProvider.GetPrimaryKey<T>();
            if (key.GetValue(entity, null) == null)
            { key.SetValue(entity, NewGuId); }
            var sql = _sqlBuilder.BuildInsertSql<T>(SchemaProvider.GetTableName<T>());
            var result = ExecuteNonQuery(sql.ToString(), entity);
            return entity;
        }

        #endregion 插入

        #region 更新

        public int Update<T>(T entity)
        {
            var key = SchemaProvider.GetPrimaryKey<T>();
            if (entity == null)
            { throw new AXDataBaseException($"不能更新 Null 实体 【{typeof(T).FullName}】"); }
            if (key.GetValue(entity, null) == null)
            { throw new AXDataBaseException($"不能更新 主键无值 实体 【{typeof(T).FullName}】"); }
            var upfield = SchemaProvider.GetUpdataProperties<T>().Where(p => p.Name != key.Name).Select(p => p.Name).ToArray();
            var sb = _sqlBuilder.BuildUpdateByIdSql<T>(SchemaProvider.GetTableName<T>(), upfield);
            var result = ExecuteNonQuery(sb.ToString(), entity);
            return result;
        }

        public int Update<T>(T entity, string fields)
        {
            var key = SchemaProvider.GetPrimaryKey<T>();
            if (entity == null)
            { throw new AXDataBaseException($"不能更新 Null 实体 【{typeof(T).FullName}】"); }
            if (key.GetValue(entity, null) == null)
            { throw new AXDataBaseException($"不能更新 主键无值 实体 【{typeof(T).FullName}】"); }
            var allField = SchemaProvider.GetUpdataProperties<T>().Where(p => p.Name != key.Name).Select(p => p.Name).ToArray();
            var upfields = new List<string>();
            var upFieldStrs = fields.Split(",");
            foreach (var item in upFieldStrs)
            {
                if (allField.Contains(item))
                { upfields.Add(item); }
            }
            var sb = _sqlBuilder.BuildUpdateByIdSql<T>(SchemaProvider.GetTableName<T>(), upfields.ToArray());
            var result = ExecuteNonQuery(sb.ToString(), entity);
            return result;
        }

        #endregion 更新

        #region 删除

        public int Delete<T>(T entity)
        {
            if (entity == null)
            { return 0; }
            var idvalue = SchemaProvider.GetPrimaryKey<T>().GetValue(entity);
            return Delete<T>(idvalue);
        }

        public int Delete<T>(string sql, params object[] args)
        {
            var sb = new StringBuilder();
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { sb = _sqlBuilder.BuildDeleteTableSqlNoWhere(SchemaProvider.GetTableName<T>()); }
            var result = ExecuteNonQuery(sb.Append(sql).ToString(), args);
            return result;
        }

        public int Delete<T>(object PrimaryKey)
        {
            if (PrimaryKey == null)
            { return 0; }
            var sb = _sqlBuilder.BuildDeleteByIdSql<T>(SchemaProvider.GetTableName<T>());
            var result = ExecuteNonQuery(sb.ToString(), PrimaryKey);
            return result;
        }

        #endregion 删除

        #region 查询

        public Boolean IsExists<T>(object id)
        {
            if (id == null)
            { return false; }
            var sb = _sqlBuilder.BuildSelectCountSql(SchemaProvider.GetTableName<T>());
            sb.AppendFormat(" AND {0} ", _sqlBuilder.GetEqualConditions(SchemaProvider.GetPrimaryKey<T>().Name).First());
            var result = ExecuteScalar<int>(sb.ToString(), new object[] { id });
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public Boolean IsExists<T>(string sql, params object[] args)
        {
            if (GetCount<T>(sql, args) > 0)
            { return true; }
            return false;
        }

        public long GetCount<T>()
        {
            var sb = _sqlBuilder.BuildSelectCountSql(SchemaProvider.GetTableName<T>());
            var result = ExecuteScalar<long>(sb.ToString(), null);
            return result;
        }

        public long GetCount<T>(string sql, params object[] args)
        {
            var sb = new StringBuilder();
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { sb = _sqlBuilder.BuildSelectCountSqlNoWhere(SchemaProvider.GetTableName<T>()); }
            var result = ExecuteScalar<long>(sb.Append(sql).ToString(), args);
            return result;
        }

        public DataTable GetDataTable(string sql, params object[] args)
        {
            var result = new DataTable();
            var cmd = _dbConnection.CreateCommand();
            CreateSqlCommand(cmd, CommandType.Text, sql, args);
            var reader = cmd.ExecuteReader();
            result.Load(reader);
            return result;
        }

        public List<T> GetList<T>()
        {
            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>());
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>();
        }

        public List<T> GetList<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>();
        }

        public T SingleOrDefault<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>().SingleOrDefault();
        }

        public T SingleOrDefaultById<T>(object id)
        {
            if (id == null)
            { return default(T); }

            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>());
            var parmName = _sqlBuilder.UseParmChar(SchemaProvider.GetPrimaryKey<T>().Name);
            sb.AppendFormat(" AND {0} = {1}", SchemaProvider.GetPrimaryKey<T>().Name, parmName);
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>().SingleOrDefault();
        }

        public T FirstOrDefault<T>(string sql, params object[] args)
        {
            if (sql.StartsWith("WHERE", StringComparison.InvariantCultureIgnoreCase))
            { var sb = _sqlBuilder.BuildSelectSqlNoWhere<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>()); }
            var datetable = GetDataTable(sql, args);
            return datetable.ToList<T>().FirstOrDefault();
        }

        public T FirstOrDefaultById<T>(object id)
        {
            if (id == null)
            { return default(T); }

            var sb = _sqlBuilder.BuildSelectSql<T>(SchemaProvider.GetTableName<T>(), SchemaProvider.GetSelectProperties<T>());
            var parmName = _sqlBuilder.UseParmChar(SchemaProvider.GetPrimaryKey<T>().Name);
            sb.AppendFormat(" AND {0} = {1}", SchemaProvider.GetPrimaryKey<T>().Name, parmName);
            var table = GetDataTable(sb.ToString(), null);
            return table.ToList<T>().FirstOrDefault();
        }

        public T CheckById<T>(object id)
        {
            if (id == null)
            { throw new AXWarringMesssageException($"请传入ID {typeof(T).Name}.{id}"); }

            var result = FirstOrDefaultById<T>(id);
            if (result == null)
            { throw new AXWarringMesssageException($"数据已过期或不存在 {typeof(T).Name}.{id}"); }
            return result;
        }

        #endregion 查询

        #region 事务

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_dbTransaction != null)
            { throw new AXDataBaseException($"已存在事务"); }
            _dbTransaction = Connection.BeginTransaction(isolationLevel);
        }

        public void CompleteTransaction()
        {
            if (_dbTransaction == null)
            { throw new AXDataBaseException($"当前无事务"); }
            _dbTransaction.Commit();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        public void AbortTransaction()
        {
            if (_dbTransaction == null)
            { throw new AXDataBaseException($"当前无事务"); }
            _dbTransaction.Rollback();
            _dbTransaction.Dispose();
            _dbTransaction = null;
        }

        #endregion 事务

        #region 结构

        public String SetSchema<T>(Boolean execute)
        {
            var result = new StringBuilder();
            var dbName = Connection.Database;
            var tableName = SchemaProvider.GetTableName<T>();
            var fields = SchemaProvider.GetSchemaProperties<T>();

            //判断表是否存在
            var exitSql = _dbConfig.GetTableExitSql(tableName, dbName);
            if (ExecuteScalar<int>(exitSql) <= 0)
            {
                result.Append(GetCreateTableSql<T>(result));
            }
            //判断字段是否存在
            else
            {
                for (int i = 0; i < fields.Count; i++)
                {
                    var item = fields[i];
                    var filedExitSql = _dbConfig.GetFiledExitSql(item.Name, tableName, dbName);
                    if (ExecuteScalar<int>(filedExitSql) <= 0)
                    {
                        result.Append(_dbConfig.GetCreateFieldSql(tableName, item));
                    }
                }
            }

            if (execute && result.Length > 0)
            { ExecuteNonQuery(result.ToString()); }
            return result.ToString();
        }

        public String GetCreateTableSql<T>(StringBuilder sb)
        {
            var tableName = _sqlBuilder.UseEscapeChar(SchemaProvider.GetTableName<T>());
            var keyName = SchemaProvider.GetPrimaryKey<T>().Name;
            var fields = SchemaProvider.GetSchemaProperties<T>();

            if (sb == null)
            { sb = new StringBuilder(); }
            sb.Append(_dbConfig.GetCreateTableSql(tableName, keyName, fields));
            return sb.ToString();
        }

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