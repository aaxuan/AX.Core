using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AX.Core.DataBase.DataRepositories
{
    public class DapperRepository
    {
        public DbConnection Connection { get; }
        public DbTransaction Transaction { get; private set; }

        public DapperRepository(DbConnection dbConnection)
        {
            Connection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
        }

        #region 事务

        public void AbortTransaction()
        {
            if (Transaction == null)
            { throw new NullReferenceException(nameof(Transaction)); }
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void CompleteTransaction()
        {
            if (Transaction == null)
            { throw new NullReferenceException(nameof(Transaction)); }
            Transaction.Commit();
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (Transaction != null)
            { throw new Exception("Transaction 对象已存在"); }
            Transaction = Connection.BeginTransaction(isolationLevel);
        }

        #endregion 事务

        public bool TestConnection()
        {
            var result = Connection.ExecuteScalar<string>("SELECT 'test' AS TEST;");
            if (string.IsNullOrWhiteSpace(result)) { return false; }
            return true;
        }

        public int ExecuteNonQuery(string sql, object arg)
        {
            return Connection.Execute(sql, arg, Transaction, GlobalDefaultSetting.CommandTimeout);
        }

        public T ExecuteScalar<T>(string sql, object arg)
        {
            return Connection.ExecuteScalar<T>(sql, arg, Transaction, GlobalDefaultSetting.CommandTimeout);
        }

        #region 增

        public T Insert<T>(T entity) where T : class
        {
            Connection.Insert<T>(entity, Transaction, GlobalDefaultSetting.CommandTimeout);
            return entity;
        }

        public List<T> BatchInsert<T>(List<T> entities) where T : class
        {
            var result = new List<T>();
            foreach (var item in entities)
            {
                result.Add(Insert(item));
            }
            return result;
        }

        #endregion 增

        #region 删

        public bool DeleteTable<T>() where T : class
        {
            return Connection.DeleteAll<T>(Transaction, GlobalDefaultSetting.CommandTimeout);
        }

        public bool Delete<T>(T entity) where T : class
        {
            return Connection.Delete<T>(entity, Transaction, GlobalDefaultSetting.CommandTimeout);
        }

        #endregion 删

        #region 改

        public bool Update<T>(T entity) where T : class
        {
            return Connection.Update<T>(entity, Transaction, GlobalDefaultSetting.CommandTimeout);
        }

        #endregion 改

        #region 查

        public List<T> GetAll<T>() where T : class
        {
            return Connection.GetAll<T>(Transaction, GlobalDefaultSetting.CommandTimeout).ToList();
        }

        public List<T> GetList<T>(string sql, object arg)
        {
            return Connection.Query<T>(sql, arg, Transaction, false, GlobalDefaultSetting.CommandTimeout).ToList();
        }

        public DataTable GetDataTable(string sql, object arg)
        {
            var result = new DataTable();
            result.Load(Connection.ExecuteReader(sql, arg, Transaction, commandTimeout: GlobalDefaultSetting.CommandTimeout));
            return result;
        }

        #endregion 查

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