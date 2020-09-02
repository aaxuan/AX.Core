using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema
{
    public interface ISchemaProvider
    {
        List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection);

        List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection);

        List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection);
    }
}