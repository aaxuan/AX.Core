using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AX.Core.DataBase
{
    /// <summary>
    /// 链接基类
    /// </summary>
    public interface IDataRepositoryConnection
    {
        DbConnection Connection { get; }

        bool TestConnection();

        int ExecuteNonQuery(string sql, params dynamic[] args);

        T ExecuteScalar<T>(string sql, params dynamic[] args);
    }

    public interface IDataRepositoryTransaction
    {
        #region 事务

        DbTransaction Transaction { get; }

        void AbortTransaction();

        void CompleteTransaction();

        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        #endregion 事务
    }

    public interface IDataRepositoryCURD
    {
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

        IEnumerable<T> GetQuery<T>();

        IEnumerable<T> GetQuery<T>(string sql, params dynamic[] args);

        DataTable GetDataTable(string sql, params dynamic[] args);

        #endregion 查
    }

    public interface IDataRepositoryAsyncCURD
    {
        #region 增

        T InsertAsync<T>(T entity);

        List<T> BatchInsertAsync<T>(List<T> entities);

        #endregion 增

        #region 删

        int DeleteTableAsync<T>();

        int DeleteAsync<T>(T entity);

        int DeleteAsync<T>(string sql, params dynamic[] args);

        #endregion 删

        #region 改

        int UpdateAsync<T>(T entity);

        int UpdateAsync<T>(T entity, string fields);

        #endregion 改

        #region 查

        bool IsExistsAsync<T>(dynamic PrimaryKey);

        bool IsExistsAsync<T>(string sql, params dynamic[] args);

        int GetCountAsync<T>();

        int GetCountAsync<T>(string sql, params dynamic[] args);

        T FirstOrDefaultAsync<T>(string sql, params dynamic[] args);

        T FirstOrDefaultByIdAsync<T>(dynamic PrimaryKey);

        T SingleOrDefaultAsync<T>(string sql, params dynamic[] args);

        List<T> GetAllAsync<T>();

        List<T> GetListAsync<T>(string sql, params dynamic[] args);

        IEnumerable<T> GetQueryAsync<T>();

        IEnumerable<T> GetQueryAsync<T>(string sql, params dynamic[] args);

        DataTable GetDataTableAsync(string sql, params dynamic[] args);

        #endregion 查
    }

    public interface IDataRepository : IDataRepositoryConnection, IDataRepositoryTransaction, IDataRepositoryCURD, IDisposable
    {
        void Save<T>(T entity);

        string GetCreateTableSql<T>();

        string UpdateSchema<T>(bool execute);
    }
}