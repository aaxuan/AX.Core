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

        DbTransaction Transaction { get; }

        #endregion 属性

        #region 事务

        void AbortTransaction();

        void CompleteTransaction();

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        #endregion 事务

        bool TestConnection();

        int ExecuteNonQuery(string sql, params dynamic[] args);

        T ExecuteScalar<T>(string sql, params dynamic[] args);

        void Save<T>(T entity);

        #region 增

        T Insert<T>(T entity);

        List<T> BatchInsert<T>(List<T> entities);

        #endregion 增

        #region 删

        int DeleteTable<T>();

        int Delete<T>(T entity);

        int Delete<T>(string sql, params dynamic[] args);

        #endregion 删

        #region 改

        int Update<T>(T entity);

        int Update<T>(T entity, string fields);

        #endregion 改

        #region 查

        bool IsExists<T>(dynamic PrimaryKey);

        bool IsExists<T>(string sql, params dynamic[] args);

        int GetCount<T>();

        int GetCount<T>(string sql, params dynamic[] args);

        T FirstOrDefault<T>(string sql, params dynamic[] args);

        T FirstOrDefaultById<T>(dynamic PrimaryKey);

        T SingleOrDefault<T>(string sql, params dynamic[] args);

        List<T> GetAll<T>();

        List<T> GetList<T>(string sql, params dynamic[] args);

        DataTable GetDataTable(string sql, params dynamic[] args);

        #endregion 查

        string GetCreateTableSql<T>();

        string UpdateSchema<T>(bool execute);
    }
}