using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    internal class SQLiteSchemaProvider : ISchemaProvider
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
            //return $"SELECT * FROM sqlite_master WHERE type='table' ORDER BY name;";
            throw new NotImplementedException();
        }
    }
}