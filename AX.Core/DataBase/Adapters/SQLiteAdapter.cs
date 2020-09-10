//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace AX.Core.DataBase.Adapters
//{
//    internal class SQLiteAdapter : IAdapter
//    {
//        //https://www.sqlite.org/lang_keywords.html

//        public string DbParmChar => "@";

//        public string LeftEscapeChar => "[";

//        public string RightEscapeChar => "]";

//        public string GetColumnExitSql(string fieldName, string tableName, string dataBaseName)
//        {
//            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}' AND sql LIKE '%{fieldName}%'";
//        }

//        public string GetCreateColumnSql(string tableName, PropertyInfo item)
//        { 
//            return $"ALTER TABLE {tableName} ADD COLUMN {item.Name.ToLower()} {GetType(item)};";
//        }

//        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
//        {
//            throw new NotImplementedException();
//        }

//        public string GetTableExitSql(string tableName, string dataBaseName)
//        {
//            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' and name = '{tableName}'";
//        }
//    }
//}


////using System;
////using System.Collections.Generic;
////using System.Reflection;
////using System.Text;

////namespace AX.Core.DataBase.Configs
////{
////    internal class SQLiteProviderConfig : IDBProviderConfig
////    {
////        //https://www.sqlite.org/lang_keywords.html


////        public string GetCreateFieldSql(string tableName, PropertyInfo item)
////        {

////        }

////        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
////        {
////            var result = new StringBuilder();
////            result.Append($"CREATE TABLE {tableName} (");
////            for (int i = 0; i < propertyInfos.Count; i++)
////            {
////                var item = propertyInfos[i];
////                result.Append($"{item.Name.ToLower()} {GetType(item)}");
////                if (i != propertyInfos.Count - 1)
////                { result.Append($","); }
////            }
////            result.Append($")");
////            return result.ToString();
////        }

////        private string GetType(PropertyInfo item)
////        {
////            switch (Type.GetTypeCode(item.PropertyType))
////            {
////                case TypeCode.Empty: break;
////                case TypeCode.Object: break;
////                case TypeCode.DBNull: break;
////                case TypeCode.UInt16:
////                case TypeCode.UInt32:
////                case TypeCode.UInt64:
////                case TypeCode.Int16:
////                case TypeCode.Int32:
////                case TypeCode.Int64:
////                case TypeCode.Byte:
////                case TypeCode.SByte:
////                case TypeCode.Boolean: return "INTEGER";
////                case TypeCode.String:
////                case TypeCode.Decimal:
////                case TypeCode.DateTime:
////                case TypeCode.Char: return "TEXT";
////                case TypeCode.Single:
////                case TypeCode.Double: return "REAL";
////                default: break;
////            }

////            if (item.PropertyType.FullName == typeof(Byte[]).FullName)
////            { return "BLOB"; }

////            return "未匹配类型";
////        }

////        public string GetLoadDBColmunSql(string dbName, string tablename)
////        {
////            throw new System.NotImplementedException();
////        }

////        public string GetLoadDBSchemasSql()
////        {
////            return null;
////        }

////        public string GetLoadDBTableSql(string dbName)
////        {

////        }

////        public string GetTableExitSql(string tableName, string dataBaseName)
////        {

////        }

////        public string GetFiledExitSql(string fieldName, string tableName, string dataBaseName)
////        {
////            return $"SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' AND name = '{tableName}' AND sql LIKE '%{fieldName}%'";
////        }
////    }
////}