using System;
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

        public string GetCreateTableSql(string tableName, string KeyName, PropertyInfo[] propertyInfos)
        {
            var result = new StringBuilder();

            result.Append($"CREATE TABLE IF NOT EXISTS {tableName} (");
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var item = propertyInfos[i];
                result.Append($"{item.Name.ToLower()} {GetType(item)}");
                if (i != propertyInfos.Length)
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

        //public List<string> LoadDB()
        //{
        //    var result = new List<string>();
        //    var allDB = new DataTable();
        //    allDB.Load(ConnForm.Connection.ExecuteReader("SHOW DATABASES"));
        //    foreach (DataRow row in allDB.Rows)
        //    {
        //        result.Add(row[0].ToString());
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 表名称
        ///// </summary>
        ///// <param name="dbName"></param>
        //public void GetDBScheml(List<string> dbNames)
        //{
        //    foreach (var dbName in dbNames)
        //    {
        //        // 添加库
        //        var db = new DataModel.SchemlDB() { CodeName = dbName, DisplayName = dbName, Description = dbName };

        //        var tablesql = $"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{dbName}'";
        //        var dbTables = new DataTable();
        //        dbTables.Load(ConnForm.Connection.ExecuteReader(tablesql));

        //        foreach (DataRow tableRow in dbTables.Rows)
        //        {
        //            // 添加表
        //            var dt = new DataModel.SchemlTable() { CodeName = tableRow["TABLE_NAME"].ToString(), DisplayName = tableRow["TABLE_NAME"].ToString(), Description = tableRow["TABLE_COMMENT"].ToString() };
        //            db.Tables.Add(dt);
        //            var columnsql = $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{dbName}' AND TABLE_NAME = '{tableRow["TABLE_NAME"].ToString()}'";
        //            var dbcolumns = new DataTable();
        //            dbcolumns.Load(ConnForm.Connection.ExecuteReader(columnsql));

        //            foreach (DataRow columnRow in dbcolumns.Rows)
        //            {
        //                //添加字段
        //                var col = new DataModel.SchemlColmun();
        //                dt.Colmuns.Add(col);
        //                col.CodeName = columnRow["COLUMN_NAME"].ToString();
        //                col.DefaultValue = columnRow["COLUMN_DEFAULT"].ToString();
        //                col.CanNullable = columnRow["IS_NULLABLE"].ToString() == "YES" ? true : false;
        //                col.IsPrimaryKey = columnRow["COLUMN_KEY"].ToString() == "PRI" ? true : false;
        //                col.DisplayName = col.Description = columnRow["COLUMN_COMMENT"].ToString();
        //                col.DBType = columnRow["DATA_TYPE"].ToString();
        //                col.Order = int.Parse(columnRow["ORDINAL_POSITION"].ToString());
        //            }
        //        }
        //        MainForm.SchemlDBs.Add(db);
        //    }
        //}
    }
}