using AX.Core.Extension;
using System;
using System.Data.Common;

namespace AX.Core.DataBase
{
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