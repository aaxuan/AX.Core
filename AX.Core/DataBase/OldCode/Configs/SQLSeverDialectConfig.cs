using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AX.Core.DataBase.Configs
{
    internal class SQLSeverDialectConfig : IDBProviderConfig
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

        public string GetLoadDBSchemasSql()
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBTableSql(string dbName)
        {
            throw new System.NotImplementedException();
        }

        public string GetLoadDBColmunSql(string dbName, string tablename)
        {
            throw new System.NotImplementedException();
        }

        //public List<string> GetDatabaseNames()
        //{
        //    List<string> list = new List<string>();
        //    using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //    {
        //        database.ResetConnectionString(this.ConnString());
        //        string sqlText = "  SELECT name FROM  master..sysdatabases WHERE name NOT IN ( 'master', 'model', 'msdb', 'tempdb', 'northwind','pubs' ) order by name  ";
        //        DataTable dataTable = database.ExecuteDataTable(sqlText);
        //        foreach (object obj in dataTable.Rows)
        //        {
        //            DataRow dataRow = (DataRow)obj;
        //            list.Add(Convert.ToString(dataRow[0]));
        //        }
        //    }
        //    return list;
        //}

        //// Token: 0x060001CC RID: 460 RVA: 0x0000B030 File Offset: 0x00009230
        //public List<string> GetTableNames(string dbName)
        //{
        //    List<string> list = new List<string>();
        //    using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //    {
        //        database.ResetConnectionString(this.ConnString(dbName));
        //        string sqlText = "\r\n                    SELECT DISTINCT\r\n                        [name]\r\n                    FROM\r\n\r\n                        sys.all_objects\r\n                    WHERE\r\n                        [type] = 'U'\r\n\r\n                        AND[name] NOT IN ('sysdiagrams')\r\n\r\n                   ORDER BY\r\n                       [name] ";
        //        DataTable dataTable = database.ExecuteDataTable(sqlText);
        //        foreach (object obj in dataTable.Rows)
        //        {
        //            DataRow dataRow = (DataRow)obj;
        //            list.Add(Convert.ToString(dataRow[0]));
        //        }
        //    }
        //    return list;
        //}

        //// Token: 0x1700008C RID: 140
        //// (get) Token: 0x060001CD RID: 461 RVA: 0x0000B0F4 File Offset: 0x000092F4
        //private XmlElement ConnConfigAll
        //{
        //	get
        //	{
        //		XmlDocument xmlDocument = new XmlDocument();
        //		xmlDocument.LoadXml("<lz><databases><add name=\"" + this.dbcode + "\" type=\"LeadingMIS.Core.Data.LZDatabaseForSqlServer, LeadingMIS.Core.Data\" connectionString=\"Data Source=168.168.1.103; Initial Catalog=LZMISTestDb; User ID=sa; Password=sa; Pooling=true\" encrypt=\"false\" /></databases></lz>");
        //		return xmlDocument.DocumentElement;
        //	}
        //}

        //// Token: 0x060001CE RID: 462 RVA: 0x0000B130 File Offset: 0x00009330
        //public DataTable GetForeignKeyColumn(string dbName, string tableName)
        //{
        //	DataTable result;
        //	using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //	{
        //		database.ResetConnectionString(this.ConnString(dbName));
        //		string query = "\tselect col.name  FROM \r\n\t\t          sysobjects c_obj\r\n\t\t          ,sysobjects t_obj\r\n\t\t          ,sysobjects r_obj\r\n\t\t          ,syscolumns col\r\n\t\t          ,sysreferences  ref\r\n\t          WHERE\r\n\t\t          c_obj.xtype IN ('F')\r\n\t\t          AND t_obj.id = c_obj.parent_obj\r\n\t\t          AND t_obj.id = col.id\r\n\t\t          AND col.colid IN \r\n\t\t          (ref.fkey1,ref.fkey2,ref.fkey3,ref.fkey4,ref.fkey5,ref.fkey6,\r\n\t\t          ref.fkey7,ref.fkey8,ref.fkey9,ref.fkey10,ref.fkey11,ref.fkey12,\r\n\t\t          ref.fkey13,ref.fkey14,ref.fkey15,ref.fkey16)\r\n\t\t          AND c_obj.id = ref.constid\r\n\t\t          AND r_obj.id = ref.rkeyid\r\n\t\t          AND t_obj.name =@tablename ";
        //		DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
        //		database.AddInParameter(sqlStringCommand, "tablename", DbType.String, tableName);
        //		result = database.ExecuteDataTable(sqlStringCommand);
        //	}
        //	return result;
        //}

        //// Token: 0x060001CF RID: 463 RVA: 0x0000B1A0 File Offset: 0x000093A0
        //public DataTable GetPrimaryKeyColumn(string dbName, string tableName)
        //{
        //	DataTable result;
        //	using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //	{
        //		database.ResetConnectionString(this.ConnString(dbName));
        //		string query = "\t     \r\n                Select   \r\n                col_name(k.id,colid)  name\r\n                From  sysobjects         as o\r\n                Inner Join sysindexes    as i On i.name=o.name \r\n                Inner Join sysindexkeys  as k On k.indid=i.indid and parent_obj = k.id\r\n                Where \r\n                o.xtype = 'PK'  and k.id=object_id(@tablename) ";
        //		DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
        //		database.AddInParameter(sqlStringCommand, "tablename", DbType.String, tableName);
        //		result = database.ExecuteDataTable(sqlStringCommand);
        //	}
        //	return result;
        //}

        //// Token: 0x060001D0 RID: 464 RVA: 0x0000B210 File Offset: 0x00009410
        //public DataTable GetColumnListByTable(string dbName, string tableName)
        //{
        //	DataTable result;
        //	using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //	{
        //		database.ResetConnectionString(this.ConnString(dbName));
        //		string query = "\t\r\n                                SELECT  \r\n                                    字段序号=a.colorder, \r\n                                    字段名=a.name, \r\n                                    中文名='',\r\n                                    属性名='',\r\n                                    代码类型='',\r\n                                    --API='',\r\n                                    DTO='',\r\n                                    说明='',\r\n                                    主键=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in ( \r\n                                    SELECT name FROM sysindexes WHERE indid in( \r\n                                    SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid \r\n                                    ))) then '1' else '' end, \r\n                                    类型=b.name, \r\n                                    占用字节数=a.length, \r\n                                    长度=COLUMNPROPERTY(a.id,a.name,'PRECISION'), \r\n                                    小数位数=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), \r\n                                    允许空=case when a.isnullable=1 then '1'else '' end, \r\n                                    默认值=isnull(e.text,''),\r\n                                    标识=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '1'else '' end,\r\n                                    字段说明=isnull(g.[value],'') \r\n                                FROM \r\n                                    syscolumns a \r\n                                    left join systypes b on a.xusertype=b.xusertype \r\n                                    inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' \r\n                                    left join syscomments e on a.cdefault=e.id \r\n                                    left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id  \r\n                                where \r\n                                    d.name=@tablename\r\n                                ";
        //		DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
        //		database.AddInParameter(sqlStringCommand, "tablename", DbType.String, tableName);
        //		DataTable dtColumnList = database.ExecuteDataTable(sqlStringCommand);
        //		result = Tools.HandleColumnListDataRowValue(dtColumnList);
        //	}
        //	return result;
        //}

        //// Token: 0x060001D1 RID: 465 RVA: 0x0000B288 File Offset: 0x00009488
        //public string GetTableDescription(string dbName, string tableName)
        //{
        //	string result;
        //	using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
        //	{
        //		database.ResetConnectionString(this.ConnString(dbName));
        //		string query = " SELECT  \r\n                                    tabledescription=f.value   \r\n                                FROM    \r\n                                    sysobjects d  left join sys.extended_properties f\r\n                                on \r\n                                    d.id=f.major_id and f.minor_id=0  and d.xtype='U' and d.name<>'dtproperties'   \r\n                                where \r\n                                    d.name=@tablename";
        //		DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
        //		database.AddInParameter(sqlStringCommand, "tablename", DbType.String, tableName);
        //		result = database.ExecuteScalarToString(sqlStringCommand);
        //	}
        //	return result;
        //}
    }
}