using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Configs
{
    internal class SqlLiteProviderConfig : IDBProviderConfig
    {
        //https://www.sqlite.org/lang_keywords.html

        public string LeftEscapeChar { get { return "["; } }

        public string RightEscapeChar { get { return "]"; } }

        public string DbParmChar { get { return "@"; } }

        public string GetCreateFieldSql(string tableName, PropertyInfo item)
        {
            return $"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)};";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            var result = new StringBuilder();
            result.Append($"CREATE TABLE {tableName} (");
            for (int i = 0; i < propertyInfos.Count; i++)
            {
                var item = propertyInfos[i];
                result.Append($"{item.Name.ToLower()} {GetType(item)}");
                if (i != propertyInfos.Count - 1)
                { result.Append($","); }
            }
            result.Append($")");
            return result.ToString();
        }

        private string GetType(PropertyInfo item)
        {
            var fieldType = string.Empty;
            if (item.PropertyType.FullName == typeof(Boolean).FullName ||
                item.PropertyType.FullName == typeof(bool).FullName ||
                item.PropertyType.FullName == typeof(Byte).FullName ||
                item.PropertyType.FullName == typeof(Int16).FullName ||
                item.PropertyType.FullName == typeof(Int32).FullName ||
                item.PropertyType.FullName == typeof(Int64).FullName ||
                item.PropertyType.FullName == typeof(SByte).FullName ||
                item.PropertyType.FullName == typeof(UInt16).FullName ||
                item.PropertyType.FullName == typeof(UInt32).FullName ||
                item.PropertyType.FullName == typeof(UInt64).FullName)
            { fieldType = " INTEGER "; }
            else if (item.PropertyType.FullName == typeof(Char).FullName ||
                item.PropertyType.FullName == typeof(DateTime).FullName ||
                item.PropertyType.FullName == typeof(DateTime?).FullName ||
                item.PropertyType.FullName == typeof(decimal).FullName ||
                item.PropertyType.FullName == typeof(decimal?).FullName ||
                item.PropertyType.FullName == typeof(Decimal).FullName ||
                item.PropertyType.FullName == typeof(Decimal?).FullName ||
                item.PropertyType.FullName == typeof(DateTimeOffset).FullName ||
                item.PropertyType.FullName == typeof(Guid).FullName ||
                item.PropertyType.FullName == typeof(String).FullName ||
                item.PropertyType.FullName == typeof(TimeSpan).FullName)
            { fieldType = " TEXT "; }
            else if (item.PropertyType.FullName == typeof(Byte[]).FullName)
            { fieldType = " BLOB "; }
            else if (item.PropertyType.FullName == typeof(Double).FullName ||
                item.PropertyType.FullName == typeof(Single).FullName)
            { fieldType = " real "; }
            else
            { fieldType = " 未匹配类型 "; }

            return fieldType;
        }

        public string GetLoadDBColmunSql(string dbName, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBSchemasSql()
        {
            return null;
        }

        public string GetLoadDBTableSql(string dbName)
        {
            return $"SELECT * FROM sqlite_master WHERE type='table' ORDER BY name;";
        }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' and name = '{tableName}'";
        }

        public string GetFiledExitSql(string fieldName, string tableName, string dataBaseName)
        {
            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}' AND sql LIKE '%{fieldName}%'";
        }
    }
}