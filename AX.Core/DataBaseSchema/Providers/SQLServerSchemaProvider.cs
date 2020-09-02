using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    internal class SQLServerSchemaProvider : ISchemaProvider
    {
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

        //public string GetLoadDBSchemasSql()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public string GetLoadDBTableSql(string dbName)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public string GetLoadDBColmunSql(string dbName, string tablename)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}