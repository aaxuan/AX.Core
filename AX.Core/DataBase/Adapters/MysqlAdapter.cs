using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Adapters
{
    public class MysqlAdapter : IAdapter
    {
        public string LeftEscapeChar { get { return "`"; } }

        public string RightEscapeChar { get { return "`"; } }

        public string DbParmChar { get { return "@"; } }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM information_schema.`TABLES` WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA = '{dataBaseName}'";
        }

        public string GetColumnExitSql(string fieldName, string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM information_schema.`COLUMNS` WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{fieldName}' AND TABLE_SCHEMA = '{dataBaseName}'";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            var result = new StringBuilder();

            result.Append($"CREATE TABLE IF NOT EXISTS {tableName} (");
            result.Append(string.Join(",", propertyInfos.Select(p => $"{p.Name.ToLower()} {GetType(p)}")));
            result.Append($",PRIMARY KEY({KeyName})");
            result.Append($")");
            result.Append($"ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COMMENT = '{tableName}';");

            return result.ToString();
        }

        public string GetCreateColumnSql(string tableName, PropertyInfo item)
        {
            return $"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)} DEFAULT NULL;";
        }

        private string GetType(PropertyInfo item)
        {
            var lowerName = item.PropertyType.FullName.ToLower();

            if (lowerName.Contains("boolean"))
            { return "bit(1)"; }
            if (lowerName.Contains("datetime"))
            { return "datetime"; }
            if (lowerName.Contains("decimal"))
            { return "decimal(10, 2)"; }
            if (lowerName.Contains("double"))
            { return "double"; }
            if (lowerName.Contains("int"))
            { return "int(11)"; }
            if (lowerName.Contains("string"))
            {
                var length = Reflection.PropertyInfoManage.GetMaxStringLength(item);
                if (length <= 0)
                { return "varchar(255)"; }
                else
                { return $"varchar({length})"; }
            }

            throw new System.NotSupportedException($"未匹配字段对应数据库类型 {item.PropertyType.FullName}");
        }
    }
}