using AX.Core.CommonModel.Exceptions;
using AX.Core.DataBase.Config;
using AX.Core.Extension;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AX.Core.DataBase.DataRepositories
{
    public class DapperRepository : IDataRepository
    {
        internal IAdapter Adapter { get; }
        internal SqlBuilder InnerSqlBuilder { get { return new SqlBuilder(Adapter.RightEscapeChar, Adapter.LeftEscapeChar, Adapter.DbParmChar); } }

        internal DynamicParameters GetDynamicParameters(string sql, dynamic[] args)
        {
            DynamicParameters result = new DynamicParameters();
            var regexStr = $"{Adapter.DbParmChar}\\w+";
            var parmars = Regex.Matches(sql, regexStr);

            //无参
            if (parmars.Count <= 0 || args == null || args.Length <= 0) { return result; }
            //多参 一对象
            if (parmars.Count > 1 && args.Length == 1)
            {
                result.AddDynamicParams(args[0]);
                return result;
            }

            if (parmars.Count != args.Length)
            {
                throw new AXDataBaseException($"参数数量错误 请检查SQL语句【{sql}】 参数【{string.Join(",", args)}】", sql);
            }

            //一一对应
            for (int i = 0; i < parmars.Count; i++)
            {
                Match parm = parmars[i];
                result.Add(parm.Value.Substring(1, parm.Value.Length - 1), args[i]);
            }
            return result;
        }

        #region 内部实现

        internal T InnerExecuteScalar<T>(string sql, params dynamic[] args)
        {
            try
            {
                TraceLog(nameof(InnerExecuteScalar), sql, args);
                return Connection.ExecuteScalar<T>(sql, GetDynamicParameters(sql, args), Transaction, GlobalConfig.CommandTimeout);
            }
            catch (Exception ex)
            {
                throw new AXDataBaseException(ex.Message, sql);
            }
        }

        internal int InnerExecuteNonQuery(string sql, params dynamic[] args)
        {
            try
            {
                TraceLog(nameof(InnerExecuteNonQuery), sql, args);
                return Connection.Execute(sql, GetDynamicParameters(sql, args), Transaction, GlobalConfig.CommandTimeout);
            }
            catch (Exception ex)
            {
                throw new AXDataBaseException(ex.Message, sql);
            }
        }

        internal T InnerQueryFirstOrDefault<T>(string sql, params dynamic[] args)
        {
            try
            {
                TraceLog(nameof(InnerQueryFirstOrDefault), sql, args);
                return Connection.QueryFirstOrDefault<T>(sql, GetDynamicParameters(sql, args), Transaction, GlobalConfig.CommandTimeout);
            }
            catch (Exception ex)
            {
                throw new AXDataBaseException(ex.Message, sql);
            }
        }

        internal T InnerQuerySingleOrDefault<T>(string sql, params dynamic[] args)
        {
            try
            {
                TraceLog(nameof(InnerQuerySingleOrDefault), sql, args);
                return Connection.QuerySingleOrDefault<T>(sql, GetDynamicParameters(sql, args), Transaction, GlobalConfig.CommandTimeout);
            }
            catch (Exception ex)
            {
                throw new AXDataBaseException(ex.Message, sql);
            }
        }

        internal IEnumerable<T> InnerQuery<T>(string sql, params dynamic[] args)
        {
            try
            {
                TraceLog(nameof(InnerQuery), sql, args);
                return Connection.Query<T>(sql, GetDynamicParameters(sql, args), Transaction, commandTimeout: GlobalConfig.CommandTimeout);
            }
            catch (Exception ex)
            {
                throw new AXDataBaseException(ex.Message, sql);
            }
        }

        internal void TraceLog(string methodname, string sql, params dynamic[] args)
        {
            if (GlobalConfig.TraceLogSql)
            {
                Trace.WriteLine("----- -----");
                Trace.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}] [{methodname}] [{sql}]");
                var parameters = GetDynamicParameters(sql, args);
                foreach (var item in parameters.ParameterNames)
                {
                    Trace.WriteLine($"[{item}]-->[{parameters.Get<object>(item)}]");
                }
            }
        }

        #endregion 内部实现

        public DapperRepository(DbConnection dbConnection)
        {
            dbConnection.CheckIsNull();
            Connection = dbConnection;
            DataBaseType = DBFactory.GetDataBaseType(Connection);
            Adapter = DBFactory.GetAdapter(DataBaseType);
        }

        #region 属性

        public DbConnection Connection { get; }

        public DbTransaction Transaction { get; private set; }

        public DataBaseType DataBaseType { get; }

        #endregion 属性

        #region 事务

        public void AbortTransaction()
        {
            if (Transaction == null)
            { throw new AXDataBaseException("Transaction 对象不存在"); }
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void CompleteTransaction()
        {
            if (Transaction == null)
            { throw new AXDataBaseException("Transaction 对象不存在"); }
            Transaction.Commit();
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Transaction != null)
            { throw new AXDataBaseException("Transaction 对象已存在"); }
            Transaction = Connection.BeginTransaction(isolationLevel);
        }

        #endregion 事务

        public bool TestConnection()
        {
            var result = InnerExecuteScalar<string>("SELECT 'test' AS test;");
            if (string.IsNullOrWhiteSpace(result))
            { return false; }
            return true;
        }

        public int ExecuteNonQuery(string sql, params dynamic[] args)
        {
            return InnerExecuteNonQuery(sql, args);
        }

        public T ExecuteScalar<T>(string sql, params dynamic[] args)
        {
            return InnerExecuteScalar<T>(sql, args);
        }

        public void Save<T>(T entity)
        {
            entity.CheckIsNull();
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var key = keyProperties.GetValue(entity);
            if (key == null || string.IsNullOrWhiteSpace(key.ToString()))
            {
                Insert<T>(entity);
            }
            else
            {
                Update<T>(entity);
            }
        }

        #region 增

        public T Insert<T>(T entity)
        {
            var type = typeof(T);
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var sql = InnerSqlBuilder.BuildInsert(tableName, allProperties).ToSql();
            InnerExecuteNonQuery(sql, entity);
            return entity;
        }

        public List<T> BatchInsert<T>(List<T> entities)
        {
            var type = typeof(T);
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var sql = InnerSqlBuilder.BuildInsert(tableName, allProperties).ToSql();
            InnerExecuteNonQuery(sql, entities);
            return entities;
        }

        #endregion 增

        #region 删

        public int DeleteTable<T>()
        {
            var tableName = TypeMaper.GetTableName<T>();
            var sql = InnerSqlBuilder.BuildDelete(tableName).ToSql();
            return InnerExecuteNonQuery(sql, null);
        }

        public int Delete<T>(T entity)
        {
            if (entity == null) { return 0; }

            var keyProperties = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var sql = InnerSqlBuilder.BuildDelete(tableName).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return InnerExecuteNonQuery(sql.ToString(), entity);
        }

        public int Delete<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildDelete(TypeMaper.GetTableName<T>()).AppendSql(sql).ToSql();
            }
            return InnerExecuteNonQuery(sql, args);
        }

        #endregion 删

        #region 改

        public int Update<T>(T entity)
        {
            var type = typeof(T);
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var noKeyProperties = allProperties.Except(new List<PropertyInfo>() { keyProperties }).ToList();
            var sql = InnerSqlBuilder.BuildUpdate(tableName, noKeyProperties).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return InnerExecuteNonQuery(sql.ToString(), entity);
        }

        public int Update<T>(T entity, string fields)
        {
            fields.CheckIsNullOrWhiteSpace();
            var type = typeof(T);
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);

            var arrayfields = fields.ToLower().Split(',');

            allProperties = allProperties.Where(p => arrayfields.Contains(p.Name.ToLower())).ToList();
            var noKeyProperties = allProperties.Except(new List<PropertyInfo>() { keyProperties }).ToList();

            var sql = InnerSqlBuilder.BuildUpdate(tableName, noKeyProperties).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return InnerExecuteNonQuery(sql.ToString(), entity);
        }

        #endregion 改

        #region 查

        public bool IsExists<T>(dynamic PrimaryKey)
        {
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var sql = InnerSqlBuilder.BuildSelectCount(TypeMaper.GetTableName<T>()).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return Convert.ToBoolean(InnerExecuteScalar<int>(sql, PrimaryKey));
        }

        public bool IsExists<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelectCount(TypeMaper.GetTableName<T>()).AppendSql(sql).ToSql();
            }
            return Convert.ToBoolean(InnerExecuteScalar<int>(sql, args));
        }

        public int GetCount<T>()
        {
            var sql = InnerSqlBuilder.BuildSelectCount(TypeMaper.GetTableName<T>()).ToSql();
            return InnerExecuteScalar<int>(sql, null);
        }

        public int GetCount<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelectCount(TypeMaper.GetTableName<T>()).AppendSql(sql).ToSql();
            }
            return InnerExecuteScalar<int>(sql, args);
        }

        public T FirstOrDefault<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).AppendSql(sql).ToSql();
            }
            return InnerQueryFirstOrDefault<T>(sql, args);
        }

        public T FirstOrDefaultById<T>(dynamic PrimaryKey)
        {
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return InnerExecuteScalar<int>(sql, PrimaryKey);
        }

        public T SingleOrDefault<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).AppendSql(sql).ToSql();
            }
            return InnerQuerySingleOrDefault<T>(sql, args);
        }

        public T SingleOrDefaultById<T>(dynamic PrimaryKey)
        {
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).Where().AppendColumnNameEqualsValue(keyProperties).ToSql();
            return InnerExecuteScalar<int>(sql, PrimaryKey);
        }

        public List<T> GetAll<T>()
        {
            var sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).ToSql();
            return InnerQuery<T>(sql, null).ToList();
        }

        public List<T> GetList<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).AppendSql(sql).ToSql();
            }
            return InnerQuery<T>(sql, null).ToList();
        }

        public IEnumerable<T> GetQuery<T>()
        {
            var sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).ToSql();
            return InnerQuery<T>(sql, null);
        }

        public IEnumerable<T> GetQuery<T>(string sql, params dynamic[] args)
        {
            if (sql.TrimStart().ToLower().StartsWith("where", StringComparison.InvariantCulture))
            {
                sql = InnerSqlBuilder.BuildSelect(TypeMaper.GetTableName<T>(), TypeMaper.GetProperties(typeof(T))).AppendSql(sql).ToSql();
            }
            return InnerQuery<T>(sql, null);
        }

        public DataTable GetDataTable(string sql, params dynamic[] args)
        {
            var result = new DataTable();
            result.Load(Connection.ExecuteReader(sql, args, Transaction, commandTimeout: GlobalConfig.CommandTimeout));
            return result;
        }

        #endregion 查

        public string GetCreateTableSql<T>()
        {
            return Adapter.GetCreateTableSql(TypeMaper.GetTableName<T>(), TypeMaper.GetSingleKey<T>().Name, TypeMaper.GetProperties(typeof(T)));
        }

        public string UpdateSchema<T>(bool execute)
        {
            var result = new StringBuilder();
            var dbName = Connection.Database;
            var tableName = TypeMaper.GetTableName<T>();
            var column = TypeMaper.GetProperties(typeof(T));

            //判断表是否存在
            var exitSql = Adapter.GetTableExitSql(tableName, dbName);
            if (ExecuteScalar<int>(exitSql) <= 0)
            {
                result.Append(GetCreateTableSql<T>());
            }
            //判断字段是否存在
            else
            {
                for (int i = 0; i < column.Count; i++)
                {
                    var item = column[i];
                    var filedExitSql = Adapter.GetColumnExitSql(item.Name, tableName, dbName);
                    if (ExecuteScalar<int>(filedExitSql) <= 0)
                    {
                        result.Append(Adapter.GetCreateColumnSql(tableName, item));
                    }
                }
            }

            if (execute && result.Length > 0)
            { InnerExecuteNonQuery(result.ToString()); }
            return result.ToString();
        }

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