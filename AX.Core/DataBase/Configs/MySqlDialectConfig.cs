using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Configs
{
    public class MySqlDialectConfig : IDBDialectConfig
    {
        public string LeftEscapeChar { get { return "`"; } }

        public string RightEscapeChar { get { return "`"; } }

        public string DbParmChar { get { return "@"; } }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM information_schema.`TABLES` WHERE TABLE_NAME = '{tableName}' AND TABLE_SCHEMA = '{dataBaseName}'";
        }

        public string GetFiledExitSql(string fieldName, string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM information_schema.`COLUMNS` WHERE TABLE_NAME = '{tableName}' AND COLUMN_NAME = '{fieldName}' AND TABLE_SCHEMA = '{dataBaseName}'";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            var result = new StringBuilder();

            result.Append($"CREATE TABLE IF NOT EXISTS {tableName} (");
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                var item = propertyInfos[i];
                result.Append($"{item.Name.ToLower()} {GetType(item)}");
                if (i != propertyInfos.Count)
                { result.Append($","); }
            }
            result.Append($"PRIMARY KEY({KeyName})");
            result.Append($")");
            result.Append($"ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COMMENT = '{tableName}';");

            return result.ToString();
        }

        public string GetCreateFieldSql(string tableName, PropertyInfo item)
        {
            return $"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)} DEFAULT NULL;";
        }

        private string GetType(PropertyInfo item)
        {
            //数值类
            if (item.PropertyType.FullName == typeof(int).FullName)
            { return "int(11)"; }
            if (item.PropertyType.FullName == typeof(double).FullName)
            { return "double"; }
            if (item.PropertyType.FullName == typeof(decimal).FullName)
            { return "decimal(10, 2)"; }
            if (item.PropertyType.FullName == typeof(decimal?).FullName)
            { return "decimal(10, 2)"; }

            if (item.PropertyType.FullName == typeof(bool).FullName)
            { return "bit(1)"; }

            //时间
            if (item.PropertyType.FullName == typeof(DateTime).FullName)
            { return "datetime"; }
            if (item.PropertyType.FullName == typeof(DateTime?).FullName)
            { return "datetime"; }

            //字符串
            if (item.PropertyType.FullName == typeof(string).FullName)
            {
                var length = Reflection.PropertyInfoManage.GetMaxStringLength(item);
                if (length <= 0)
                { return "varchar(255)"; }
                else
                { return $"varchar({length})"; }
            }

            return "未匹配类型";
        }

        public string GetLoadDBSchemasSql()
        {
            return $"SHOW DATABASES";
        }

        public string GetLoadDBTableSql(string dbName)
        {
            return $"SELECT* FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{dbName}';";
        }

        public string GetLoadDBColmunSql(string dbName, string tablename)
        {
            return $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{dbName}' AND TABLE_NAME = '{tablename}';";
        }
    }
}