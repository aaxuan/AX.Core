using AX.Core.Extension;
using System;
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
            }
            throw new NotSupportedException();
        }
    }
}