using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    internal class SQLiteSchemaProvider : ISchemaProvider
    {
        //public string GetLoadDBColmunSql(string dbName, string tablename)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public string GetLoadDBSchemasSql()
        //{
        //    return null;
        //}

        //public string GetLoadDBTableSql(string dbName)
        //{
        //    return $"SELECT * FROM sqlite_master WHERE type='table' ORDER BY name;";
        //}
        public List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}