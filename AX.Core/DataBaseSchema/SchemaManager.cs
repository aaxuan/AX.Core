using AX.Core.DataBase;
using AX.Core.DataBaseSchema.Providers;
using System;

namespace AX.Core.DataBaseSchema
{
    public static class SchemaManager
    {
        public static ISchemaProvider GetSchemaProvider(DataBaseType dataBaseType)
        {
            switch (dataBaseType)
            {
                case DataBaseType.None: throw new NotSupportedException();
                case DataBaseType.MySql: return new MySqlSchemaProvider();
                //case DataBaseType.SqlLite: return new SQLiteSchemaProvider();
                default: throw new NotSupportedException();
            }
            throw new NotSupportedException();
        }
    }
}