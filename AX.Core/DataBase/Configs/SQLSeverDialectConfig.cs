using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Configs
{
    internal class SQLSeverDialectConfig : IDBDialectConfig
    {
        public string LeftEscapeChar { get { return "["; } }

        public string RightEscapeChar { get { return "]"; } }

        public string DbParmChar { get { return "@"; } }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) From sysobjects WHERE name = '{tableName}'  AND xtype = 'u'";
        }

        public string GetFiledExitSql(string fieldName, string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM syscolumns WHERE id = object_id('{tableName}') AND name = '{ fieldName }'";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            var result = new StringBuilder();

            // 新建表
            result.Append($"create table {tableName}(");
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                var item = propertyInfos[i];
                result.Append($"{item.Name.ToLower()} {GetType(item)}");
                if (i != propertyInfos.Count)
                { result.Append($","); }
            }
            result.Append($")");

            return result.ToString();
        }

        public string GetCreateFieldSql(string tableName, PropertyInfo item)
        {
            return $"alter table {tableName} add {item.Name.ToLower()} {GetType(item)};";
        }

        private static string GetType(PropertyInfo colItem)
        {
            var typename = colItem.PropertyType.Name.ToString().ToLower();

            if (typename.Contains("string"))
            {
                return "varchar(2000)";
            }
            if (typename.Contains("int"))
            {
                return "bigint";
            }
            if (typename.Contains("datetime"))
            {
                return "datetime";
            }

            return string.Empty;
        }
    }
}