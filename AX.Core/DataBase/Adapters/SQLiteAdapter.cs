using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Adapters
{
    internal class SQLiteAdapter : IAdapter
    {
        //https://www.sqlite.org/lang_keywords.html

        public string DbParmChar => "@";

        public string LeftEscapeChar => "[";

        public string RightEscapeChar => "]";

        public string GetColumnExitSql(string fieldName, string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}' AND sql LIKE '%{fieldName}%'";
        }

        public string GetCreateColumnSql(string tableName, PropertyInfo item)
        {
            return $"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)};";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            var result = new StringBuilder();
            result.Append($"CREATE TABLE {tableName} (");
            result.Append(string.Join(",", propertyInfos.Select(p => $"{p.Name.ToLower()} {GetType(p)}")));
            result.Append($")");
            return result.ToString();
        }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' and name = '{tableName}'";
        }

        private string GetType(PropertyInfo item)
        {
            var lowerName = item.PropertyType.FullName.ToLower();

            if (lowerName.Contains("boolean"))
            { return "INTEGER"; }
            if (lowerName.Contains("datetime"))
            { return "TEXT"; }
            if (lowerName.Contains("decimal"))
            { return "TEXT"; }
            if (lowerName.Contains("double"))
            { return "REAL"; }
            if (lowerName.Contains("int"))
            { return "INTEGER"; }
            if (lowerName.Contains("string"))
            {
                var length = Reflection.PropertyInfoManage.GetMaxStringLength(item);
                if (length <= 0)
                { return "TEXT"; }
                else
                { return $"TEXT"; }
            }

            throw new System.NotSupportedException($"未匹配字段对应数据库类型 {item.PropertyType.FullName}");
        }
    }
}