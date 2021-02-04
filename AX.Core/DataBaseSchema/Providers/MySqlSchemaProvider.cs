using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    public class MySqlSchemaProvider : ISchemaProvider
    {
        public List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection)
        {
            dbConnection.TryOpen();
            var result = new List<SchemaDB>();
            var table = new DataTable();
            table.Load(dbConnection.ExecuteReader("SHOW DATABASES;"));
            foreach (DataRow row in table.Rows)
            {
                var resultItem = new SchemaDB();
                resultItem.CodeName = row[0].ToString();
                resultItem.Description = row[0].ToString();
                resultItem.DisplayName = row[0].ToString();
                resultItem.ConnectionString = dbConnection.ConnectionString;
                result.Add(resultItem);
            }
            return result;
        }

        public List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection)
        {
            dbConnection.TryOpen();
            var result = new List<SchemaTable>();
            var table = new DataTable();
            table.Load(dbConnection.ExecuteReader($"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schemaDB.CodeName}';"));
            foreach (DataRow row in table.Rows)
            {
                var resultItem = new SchemaTable();
                resultItem.CodeName = row["TABLE_NAME"].ToString();
                resultItem.Description = row["TABLE_COMMENT"].ToString();
                resultItem.DisplayName = row["TABLE_NAME"].ToString();
                result.Add(resultItem);
            }
            return result;
        }

        public List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection)
        {
            dbConnection.TryOpen();
            var result = new List<SchemaColumn>();
            var table = new DataTable();
            table.Load(dbConnection.ExecuteReader($"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{schemaDB.CodeName}' AND TABLE_NAME = '{schemaTable.CodeName}';"));
            foreach (DataRow row in table.Rows)
            {
                //https://www.cnblogs.com/zhihuifan10/p/12124587.html
                // information_schema.COLUMNS 字段说明：
                //table_schema 表所有者（对于schema的名称）
                //table_name 表名
                //column_name 列名
                //ordinal_position 列标识号
                //column_default 列的默认值
                //is_nullable 列的为空性。如果列允许 null，那么该列返回 yes。否则，返回 no
                //data_type 系统提供的数据类型
                //character_maximum_length 以字符为单位的最大长度，适于二进制数据、字符数据，或者文本和图像数据。否则，返回 null。有关更多信息，请参见数据类型
                //character_octet_length      以字节为单位的最大长度，适于二进制数据、字符数据，或者文本和图像数据。否则，返回 NULL。
                //numeric_precision 近似数字数据、精确数字数据、整型数据或货币数据的精度。否则，返回 null
                //numeric_precision_radix 近似数字数据、精确数字数据、整型数据或货币数据的精度基数。否则，返回 null
                //numeric_scale 近似数字数据、精确数字数据、整数数据或货币数据的小数位数。否则，返回 null
                //datetime_precision datetime 及 sql-92 interval 数据类型的子类型代码。对于其它数据类型，返回 null
                //character_set_catalog 如果列是字符数据或 text 数据类型，那么返回 master，指明字符集所在的数据库。否则，返回 null
                //character_set_schema 如果列是字符数据或 text 数据类型，那么返回 dbo，指明字符集的所有者名称。否则，返回 null
                //character_set_name 如果该列是字符数据或 text 数据类型，那么为字符集返回唯一的名称。否则，返回 null
                //collation_catalog 如果列是字符数据或 text 数据类型，那么返回 master，指明在其中定义排序次序的数据库。否则此列为 null
                //collation_schema 返回 dbo，为字符数据或 text 数据类型指明排序次序的所有者。否则，返回 null
                //collation_name 如果列是字符数据或 text 数据类型，那么为排序次序返回唯一的名称。否则，返回 null。
                //domain_catalog 如果列是一种用户定义数据类型，那么该列是某个数据库名称，在该数据库名中创建了这种用户定义数据类型。否则，返回 null
                //domain_schema 如果列是一种用户定义数据类型，那么该列是这种用户定义数据类型的创建者。否则，返回 null
                //domain_name 如果列是一种用户定义数据类型，那么该列是这种用户定义数据类型的名称。否则，返回 NULL

                var resultItem = new SchemaColumn();
                resultItem.CodeName = row["COLUMN_NAME"].ToString();
                resultItem.DefaultValue = row["COLUMN_DEFAULT"].ToString();
                resultItem.CanNullable = row["IS_NULLABLE"].ToString() == "YES" ? true : false;
                resultItem.IsPrimaryKey = row["COLUMN_KEY"].ToString() == "PRI" ? true : false;
                resultItem.DisplayName = resultItem.Description = row["COLUMN_COMMENT"].ToString();
                resultItem.DBType = row["DATA_TYPE"].ToString();
                resultItem.Order = int.Parse(row["ORDINAL_POSITION"].ToString());
                result.Add(resultItem);
            }
            return result;
        }
    }
}