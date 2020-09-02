using AX.Core.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    public class MySqlSchemaProvider : ISchemaProvider
    {
        public List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection)
        {
            //return $"SHOW DATABASES";
            throw new NotSupportedException();
        }

        public List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection)
        {
            schemaDB.CodeName.CheckIsNullOrWhiteSpace();
            // return $"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{dbName}';";
            throw new NotSupportedException();
        }

        public List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection)
        {
            schemaDB.CodeName.CheckIsNullOrWhiteSpace();
            schemaTable.CodeName.CheckIsNullOrWhiteSpace();
            //return $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{dbName}' AND TABLE_NAME = '{tablename}';";
            throw new NotSupportedException();
        }
    }
}