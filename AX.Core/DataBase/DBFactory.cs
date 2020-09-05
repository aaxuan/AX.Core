using AX.Core.Extension;
using System;
using System.Data;
using System.Data.Common;

namespace AX.Core.DataBase
{
    public enum DataBaseType
    {
        None = 0,
        MySql = 1,
        SQLite = 2,
    }

    public static class DBFactory
    {
        public static IDataRepository GetDataRepository(DbConnection dbConnection)
        {
            dbConnection.CheckIsNull();
            if (AxCoreGlobalSettings.DataBaseUseDapper)
            {
                return new DataBase.DataRepositories.DapperRepository(dbConnection);
            }
            throw new NotSupportedException();
        }

        public static DataBaseType GetDataBaseType(IDbConnection dbConnection)
        {
            var typeName = dbConnection.GetType().Name.ToLower();

            if (typeName.Contains("mysql"))
            { return DataBaseType.MySql; }
            if (typeName.Contains("sqlite"))
            { return DataBaseType.SQLite; }

            //["sqlconnection"]
            //["sqlceconnection"]
            //["npgsqlconnection"]
            //["fbconnection"]

            return DataBaseType.None;
        }

        public static IAdapter GetAdapter(DataBaseType dataBaseType)
        {
            switch (dataBaseType)
            {
                case DataBaseType.None: return null;
                case DataBaseType.MySql: return new Adapters.MysqlAdapter();
                case DataBaseType.SQLite: throw new NotSupportedException();
                default: return null;
            }
        }
    }
}