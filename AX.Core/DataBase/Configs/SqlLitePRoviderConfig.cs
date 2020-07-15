using System.Collections.Generic;
using System.Reflection;

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
            throw new System.NotImplementedException();
            //return "ALTER TABLE t1 ADD COLUMN age int";
        }

        public string GetCreateTableSql(string tableName, string KeyName, List<PropertyInfo> propertyInfos)
        {
            throw new System.NotImplementedException();
        }

        public string GetFiledExitSql(string fieldName, string tableName, string dataBaseName)
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBColmunSql(string dbName, string tablename)
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBSchemasSql()
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBTableSql(string dbName)
        {
            throw new System.NotImplementedException();
        }

        public string GetTableExitSql(string tableName, string dataBaseName)
        {
            throw new System.NotImplementedException();
        }
    }
}