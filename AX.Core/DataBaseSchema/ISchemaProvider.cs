using System.Collections.Generic;
using System.Data.Common;

namespace AX.Core.DataBaseSchema
{
    public interface ISchemaProvider
    {
        /// <summary>
        /// 从链接加载所有库
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection);

        /// <summary>
        /// 从链接加载某库所有表
        /// </summary>
        /// <param name="schemaDB"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection);

        /// <summary>
        /// 从链接加载某库某表所有字段
        /// </summary>
        /// <param name="schemaDB"></param>
        /// <param name="schemaTable"></param>
        /// <param name="dbConnection"></param>
        /// <returns></returns>
        List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection);
    }
}