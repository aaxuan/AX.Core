using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Configs
{
    public class MySqlProviderConfig : IDBProviderConfig
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
                if (i != propertyInfos.Count - 1)
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
            switch (Type.GetTypeCode(item.PropertyType))
            {
                case TypeCode.Boolean: return "bit(1)";
                case TypeCode.Byte: break;
                case TypeCode.Char: break;
                case TypeCode.DateTime: return "datetime";
                case TypeCode.DBNull: break;
                case TypeCode.Decimal: return "decimal(10, 2)";
                case TypeCode.Double: return "double";
                case TypeCode.Empty: break;
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64: return "int(11)";
                case TypeCode.Object: break;
                case TypeCode.SByte: break;
                case TypeCode.Single: break;
                case TypeCode.String:
                    {
                        var length = Reflection.PropertyInfoManage.GetMaxStringLength(item);
                        if (length <= 0)
                        { return "varchar(255)"; }
                        else
                        { return $"varchar({length})"; }
                    }
                case TypeCode.UInt16: break;
                case TypeCode.UInt32: break;
                case TypeCode.UInt64: break;
                default: break;
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