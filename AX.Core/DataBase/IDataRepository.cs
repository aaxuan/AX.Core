using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AX.Core.DataBase
{
    public interface IDataRepository : IDisposable
    {
        #region 属性

        DbConnection Connection { get; }

        #endregion 属性

        #region 事务

        void AbortTransaction();

        void CompleteTransaction();

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        #endregion 事务

        bool TestConnection();

        int ExecuteNonQuery(string sql, params object[] args);

        T ExecuteScalar<T>(string sql, params object[] args);

        void Save<T>(T entity);

        #region 增

        T Insert<T>(T entity);

        #endregion 增

        #region 删

        int DeleteTable<T>();

        int Delete<T>(T entity);

        int Delete<T>(object PrimaryKey);

        int Delete<T>(string sql, params object[] args);

        #endregion 删

        #region 改

        int Update<T>(T entity);

        int Update<T>(T entity, string fields);

        #endregion 改

        #region 查

        bool IsExists<T>(object id);

        bool IsExists<T>(string sql, params object[] args);

        int GetCount<T>();

        int GetCount<T>(string sql, params object[] args);

        T FirstOrDefault<T>(string sql, params object[] args);

        T FirstOrDefaultById<T>(object id);

        T SingleOrDefault<T>(string sql, params object[] args);

        T SingleOrDefaultById<T>(object id);

        List<T> GetList<T>();

        List<T> GetList<T>(string sql, params object[] args);

        DataTable GetDataTable(string sql, params object[] args);

        #endregion 查

        string GetCreateTableSql<T>();

        string UpdateSchema<T>(bool execute);
    }
}