//using System;
//using System.Collections.Generic;
//using System.Reflection;

//namespace AX.Core.DataBase
//{
//    public interface IDBProviderConfig
//    {
//        String LeftEscapeChar { get; }

//        String RightEscapeChar { get; }

//        String DbParmChar { get; }

//        String GetTableExitSql(String tableName, String dataBaseName);

//        String GetFiledExitSql(String fieldName, String tableName, String dataBaseName);

//        String GetCreateTableSql(String tableName, String KeyName, List<PropertyInfo> propertyInfos);

//        String GetCreateFieldSql(String tableName, PropertyInfo item);
//    }
//}