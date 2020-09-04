using AX.Core.DataBase.Config;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.DataRepositories
{
    public class DapperRepository : IDataRepository
    {
        #region 属性

        public DbConnection Connection { get; }

        public DbTransaction Transaction { get; }

        #endregion 属性

        #region 事务

        public void AbortTransaction()
        { throw new NotSupportedException(); }

        public void CompleteTransaction()
        { throw new NotSupportedException(); }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        { throw new NotSupportedException(); }

        #endregion 事务

        public bool TestConnection() => throw new NotSupportedException();

        public int ExecuteNonQuery(string sql, params dynamic[] args) => throw new NotSupportedException();

        public T ExecuteScalar<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public void Save<T>(T entity) => throw new NotSupportedException();

        #region 增

        public T Insert<T>(T entity)
        {
            var type = typeof(T);
            var key = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var sbColumnList = string.Join(",", allProperties.Select(p => p.Name));
            var sbParameterList = string.Join(",", allProperties.Select(p => "@" + p.Name));
            var sql = $"insert into {tableName} ({sbColumnList}) values ({sbParameterList})";
            Connection.Execute(sql, entity, Transaction, GlobalConfig.CommandTimeout);
            return entity;
        }

        public List<T> BatchInsert<T>(List<T> entities)
        {
            var type = typeof(T);
            var tablename = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var sbColumnList = string.Join(",", allProperties.Select(p => p.Name));
            var sbParameterList = string.Join(",", allProperties.Select(p => "@" + p.Name));
            var wasClosed = Connection.State == ConnectionState.Closed;
            if (wasClosed) Connection.Open();
            var cmd = $"insert into {tablename} ({sbColumnList}) values ({sbParameterList})";
            Connection.Execute(cmd, entities, Transaction, GlobalConfig.CommandTimeout);
            if (wasClosed) Connection.Close();
            return entities;
        }

        #endregion 增

        #region 删

        public int DeleteTable<T>()
        {
            var tableName = TypeMaper.GetTableName<T>();
            var sql = $"delete from {tableName}";
            return Connection.Execute(sql, null, Transaction, GlobalConfig.CommandTimeout);
        }

        public int Delete<T>(T entity)
        {
            if (entity == null) { return 0; }

            var keyProperties = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();

            var sql = new StringBuilder();
            sql.AppendFormat("delete from {0} where ", tableName);
            sql.AppendFormat(" where {0} = @{0}", keyProperties.Name);
            return Connection.Execute(sql.ToString(), entity, Transaction, GlobalConfig.CommandTimeout);
        }

        public int Delete<T>(dynamic PrimaryKey) => throw new NotSupportedException();

        public int Delete<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        #endregion 删

        #region 改

        public int Update<T>(T entity)
        {
            var type = typeof(T);
            var keyProperties = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var allProperties = TypeMaper.GetProperties(type);
            var noKeyProperties = allProperties.Except(new List<PropertyInfo>() { keyProperties });

            var sql = new StringBuilder();
            sql.AppendFormat("update {0} set ", tableName);
            sql.Append(string.Join(",", noKeyProperties.Select(p => string.Format(" {0} = @{0}", p.Name))));
            sql.AppendFormat(" where {0} = @{0}", keyProperties.Name);

            return Connection.Execute(sql.ToString(), entity, commandTimeout: GlobalConfig.CommandTimeout, transaction: Transaction);

        }

        public int Update<T>(T entity, string fields) => throw new NotSupportedException();

        #endregion 改

        #region 查

        public bool IsExists<T>(dynamic PrimaryKey) => throw new NotSupportedException();

        public bool IsExists<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public int GetCount<T>() => throw new NotSupportedException();

        public int GetCount<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public T FirstOrDefault<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public T FirstOrDefaultById<T>(dynamic PrimaryKey) => throw new NotSupportedException();

        public T SingleOrDefaultById<T>(dynamic PrimaryKey)
        {
            var key = TypeMaper.GetSingleKey<T>();
            var tableName = TypeMaper.GetTableName<T>();
            var sql = $"select * from {tableName} where {key.Name} = @id";
            var dynParams = new DynamicParameters();
            dynParams.Add("@id", PrimaryKey);
            T result = default;
            result = Connection.Query<T>(sql, dynParams, Transaction, commandTimeout: Config.GlobalConfig.CommandTimeout).FirstOrDefault();
            return result;
        }

        public T SingleOrDefault<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public List<T> GetAll<T>()
        {
            var tableName = TypeMaper.GetTableName<T>();
            var sql = $"select * from {tableName}";
            return Connection.Query<T>(sql, null, Transaction, commandTimeout: Config.GlobalConfig.CommandTimeout).ToList();
        }

        public List<T> GetList<T>(string sql, params dynamic[] args) => throw new NotSupportedException();

        public DataTable GetDataTable(string sql, params dynamic[] args) => throw new NotSupportedException();

        #endregion 查

        public string GetCreateTableSql<T>() => throw new NotSupportedException();

        public string UpdateSchema<T>(bool execute) => throw new NotSupportedException();
    }
}