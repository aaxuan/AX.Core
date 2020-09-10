//namespace AX.Core.DataBase.Configs
//{
//    //internal class PostgreSqlDialectConfig
//    //{
//    //}

//    //	// Token: 0x1700007E RID: 126
//    //	// (get) Token: 0x0600018F RID: 399 RVA: 0x00009EB5 File Offset: 0x000080B5
//    //	// (set) Token: 0x0600018E RID: 398 RVA: 0x00009EAC File Offset: 0x000080AC
//    //	public string datasource { get; set; }

//    //	// Token: 0x1700007F RID: 127
//    //	// (get) Token: 0x06000191 RID: 401 RVA: 0x00009EC6 File Offset: 0x000080C6
//    //	// (set) Token: 0x06000190 RID: 400 RVA: 0x00009EBD File Offset: 0x000080BD
//    //	public string userid { get; set; }

//    //	// Token: 0x17000080 RID: 128
//    //	// (get) Token: 0x06000193 RID: 403 RVA: 0x00009ED7 File Offset: 0x000080D7
//    //	// (set) Token: 0x06000192 RID: 402 RVA: 0x00009ECE File Offset: 0x000080CE
//    //	public string password { get; set; }

//    //	// Token: 0x17000081 RID: 129
//    //	// (get) Token: 0x06000195 RID: 405 RVA: 0x00009EE8 File Offset: 0x000080E8
//    //	// (set) Token: 0x06000194 RID: 404 RVA: 0x00009EDF File Offset: 0x000080DF
//    //	public string dbname { get; set; }

//    //	// Token: 0x06000197 RID: 407 RVA: 0x00009F08 File Offset: 0x00008108
//    //	private string ConnString()
//    //	{
//    //		return this.ConnString("postgres");
//    //	}

//    //	// Token: 0x06000198 RID: 408 RVA: 0x00009F28 File Offset: 0x00008128
//    //	private string ConnString(string database)
//    //	{
//    //		string text = "5432";
//    //		bool flag = this.datasource.IndexOf(":") == -1;
//    //		string text2;
//    //		if (flag)
//    //		{
//    //			text2 = this.datasource;
//    //		}
//    //		else
//    //		{
//    //			string[] array = this.datasource.Split(new char[]
//    //			{
//    //					':'
//    //			});
//    //			text2 = array[0];
//    //			bool flag2 = array.Length == 2;
//    //			if (flag2)
//    //			{
//    //				text = array[1];
//    //			}
//    //		}
//    //		return string.Format("Host={0};Database={1};Username={2};Password={3};Port={4};", new object[]
//    //		{
//    //				text2,
//    //				database,
//    //				this.userid,
//    //				this.password,
//    //				text
//    //		});
//    //	}

//    //	// Token: 0x06000199 RID: 409 RVA: 0x00009FC5 File Offset: 0x000081C5
//    //	public void Init()
//    //	{
//    //		DatabaseFactory.Init(this.ConnConfigAll);
//    //		LZDatabaseSelect.SetSections(this.dbcode, this.ConnConfigAll.SelectSingleNode("//add"));
//    //	}

//    //	// Token: 0x0600019A RID: 410 RVA: 0x00009FF0 File Offset: 0x000081F0
//    //	public DataTable GetTableDataForPreview(string dbName, string tableName, string topNumber, string condition)
//    //	{
//    //		string text = string.Format("SELECT * FROM {1} ", topNumber, tableName);
//    //		bool flag = !string.IsNullOrEmpty(condition);
//    //		if (flag)
//    //		{
//    //			text = text + " WHERE " + condition;
//    //		}
//    //		DataTable result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			result = database.ExecuteDataTable(text);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x0600019B RID: 411 RVA: 0x0000A06C File Offset: 0x0000826C
//    //	public long GetTableDataCountForPreview(string dbName, string tableName)
//    //	{
//    //		string sqlText = string.Format("SELECT count(0) as mycount FROM {0} ", tableName);
//    //		long result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			result = (long)database.ExecuteScalarToInt(sqlText);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x0600019C RID: 412 RVA: 0x0000A0C8 File Offset: 0x000082C8
//    //	public string ConnectionTest()
//    //	{
//    //		try
//    //		{
//    //			using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //			{
//    //				database.ResetConnectionString(this.ConnString());
//    //				string text = database.ExecuteScalarToString(" Select 'test' as test ");
//    //				bool flag = text == null;
//    //				if (flag)
//    //				{
//    //					return "服务器连接出错";
//    //				}
//    //			}
//    //		}
//    //		catch (Exception ex)
//    //		{
//    //			return string.Concat(new string[]
//    //			{
//    //					"客户服务器连接出错！[",
//    //					ex.Message,
//    //					" - ",
//    //					ex.Source,
//    //					"]"
//    //			});
//    //		}
//    //		return "OK";
//    //	}

//    //	// Token: 0x0600019D RID: 413 RVA: 0x0000A184 File Offset: 0x00008384
//    //	public List<string> GetDatabaseNames()
//    //	{
//    //		List<string> list = new List<string>();
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString());
//    //			string sqlText = "  select datname as name from pg_database where datname not in ('template1','template0','edb','postgres','test') ";
//    //			DataTable dataTable = database.ExecuteDataTable(sqlText);
//    //			foreach (object obj in dataTable.Rows)
//    //			{
//    //				DataRow dataRow = (DataRow)obj;
//    //				list.Add(Convert.ToString(dataRow[0]));
//    //			}
//    //		}
//    //		return list;
//    //	}

//    //	// Token: 0x0600019E RID: 414 RVA: 0x0000A248 File Offset: 0x00008448
//    //	public List<string> GetTableNames(string dbName)
//    //	{
//    //		List<string> list = new List<string>();
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			string query = " select Table_Name as name from information_schema.TABLES where  table_schema='public' and table_catalog=@databasename order by Table_Name";
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
//    //			database.AddInParameter(sqlStringCommand, "databasename", DbType.String, dbName);
//    //			DataTable dataTable = database.ExecuteDataTable(sqlStringCommand);
//    //			foreach (object obj in dataTable.Rows)
//    //			{
//    //				DataRow dataRow = (DataRow)obj;
//    //				list.Add(Convert.ToString(dataRow[0]));
//    //			}
//    //		}
//    //		return list;
//    //	}

//    //	// Token: 0x17000082 RID: 130
//    //	// (get) Token: 0x0600019F RID: 415 RVA: 0x0000A328 File Offset: 0x00008528
//    //	private XmlElement ConnConfigAll
//    //	{
//    //		get
//    //		{
//    //			XmlDocument xmlDocument = new XmlDocument();
//    //			xmlDocument.LoadXml("<lz><databases><add name=\"" + this.dbcode + "\" type=\"LeadingMIS.Core.Data.LZDatabaseForPostgreSql, LeadingMIS.Core.Data\" connectionString=\"Data Source=168.168.1.103; Initial Catalog=LZMISTestDb; User ID=sa; Password=sa; Pooling=true\" encrypt=\"false\" /></databases></lz>");
//    //			return xmlDocument.DocumentElement;
//    //		}
//    //	}

//    //	// Token: 0x060001A0 RID: 416 RVA: 0x0000A364 File Offset: 0x00008564
//    //	public DataTable GetForeignKeyColumn(string dbName, string tableName)
//    //	{
//    //		DataTable result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			string text = "\t   \r\n \r\n   select pg_attribute.attname as name  from \r\npg_constraint  inner join pg_class \r\non pg_constraint.conrelid = pg_class.oid \r\ninner join pg_attribute on pg_attribute.attrelid = pg_class.oid \r\nand  pg_attribute.attnum = pg_constraint.conkey[1]\r\ninner join pg_type on pg_type.oid = pg_attribute.atttypid\r\nwhere pg_class.relname = '{tablename}'\r\nand pg_constraint.contype='f' \r\n \r\n                    ; ";
//    //			text = text.Replace("{tablename}", tableName);
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(text);
//    //			result = database.ExecuteDataTable(sqlStringCommand);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x060001A1 RID: 417 RVA: 0x0000A3D0 File Offset: 0x000085D0
//    //	public DataTable GetPrimaryKeyColumn(string dbName, string tableName)
//    //	{
//    //		DataTable result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			string text = "\t   \r\n   select pg_attribute.attname as name  from \r\npg_constraint  inner join pg_class \r\non pg_constraint.conrelid = pg_class.oid \r\ninner join pg_attribute on pg_attribute.attrelid = pg_class.oid \r\nand  pg_attribute.attnum = pg_constraint.conkey[1]\r\ninner join pg_type on pg_type.oid = pg_attribute.atttypid\r\nwhere pg_class.relname = '{tablename}'\r\nand pg_constraint.contype='p' \r\n                    ; ";
//    //			text = text.Replace("{tablename}", tableName);
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(text);
//    //			result = database.ExecuteDataTable(sqlStringCommand);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x060001A2 RID: 418 RVA: 0x0000A43C File Offset: 0x0000863C
//    //	public DataTable GetColumnListByTable(string dbName, string tableName)
//    //	{
//    //		DataTable result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			string text = "\t \r\nselect\r\n \r\n\t'' as 中文名,\r\n\t'' as 属性名,\r\n\t'' as 代码类型,\r\n\t'' as DTO,\r\n\t'' as 说明,\r\n\tattnum as 字段序号, \r\n\tattname as 字段名,\r\n\t\r\n\tcase when exists(select pg_attribute.attname as colname from \r\npg_constraint  inner join pg_class \r\non pg_constraint.conrelid = pg_class.oid \r\ninner join pg_attribute on pg_attribute.attrelid = pg_class.oid \r\nand  pg_attribute.attnum = pg_constraint.conkey[1]\r\ninner join pg_type on pg_type.oid = pg_attribute.atttypid\r\nwhere pg_class.relname = '{tablename}' \r\n\t\t\t\tand pg_attribute.attname =b.attname\r\nand pg_constraint.contype='p' )\r\n\t then 1\r\n\t else 0 end 主键,\r\n\t\r\n\tcase typname\r\n\twhen '_bpchar' then 'char'\r\n\twhen '_varchar' then 'varchar'\r\n\twhen '_date' then 'date'\r\n\twhen '_float8' then 'float'\r\n\twhen '_int4' then 'int'\r\n\twhen '_interval' then 'interval'\r\n\twhen '_numeric' then 'numeric'\r\n\twhen '_float4' then 'float'\r\n\twhen '_int8' then 'bigint'\r\n\twhen '_int2' then 'smallint'\r\n\twhen '_text' then 'text'\r\n\twhen '_time' then 'time'\r\n\twhen '_timestamp' then 'datetime'\r\n\twhen '_bytea' then 'byte[]'\r\n\tend as 类型,\r\n\tcase typname\r\n\twhen '_bpchar' then atttypmod - 4\r\n\twhen '_varchar' then atttypmod - 4\r\n\twhen '_numeric' then (atttypmod - 4) / 65536\r\n\telse attlen\r\n\tend as 长度,\r\n\tcase typname\r\n\twhen '_numeric' then (atttypmod - 4) % 65536\r\n\telse 0\r\n\tend as 小数位数 \r\n\t,col_description(b.attrelid,b.attnum) as 字段说明\r\nfrom \r\n\tpg_stat_user_tables as a,\r\n\tpg_attribute as b,\r\n\tpg_type as c \r\nwhere \r\n\tschemaname='public'\r\n\tand relname='{tablename}' \r\n\tand a.relid=b.attrelid\r\n\tand b.attnum>0\r\n\tand b.atttypid=c.typelem\r\n\tand substr(typname,1,1)='_'\r\n    and attisdropped=false\r\norder by schemaname,relname,attnum;\r\n                ";
//    //			text = text.Replace("{tablename}", tableName);
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(text);
//    //			DataTable dtColumnList = database.ExecuteDataTable(sqlStringCommand);
//    //			result = Tools.HandleColumnListDataRowValue(dtColumnList);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x060001A3 RID: 419 RVA: 0x0000A4B0 File Offset: 0x000086B0
//    //	public string GetTableDescription(string dbName, string tableName)
//    //	{
//    //		string result;
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			string query = " select cast(obj_description(relfilenode,'pg_class') as varchar) as TABLE_COMMENT from pg_class c where relname in (select tablename from pg_tables where schemaname='public' and position('_2' in tablename)=0) and relname = '" + tableName + "'";
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
//    //			result = database.ExecuteScalarToString(sqlStringCommand);
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x060001A4 RID: 420 RVA: 0x0000A518 File Offset: 0x00008718
//    //	public DataTable GetSQLParseResult(string dbName, string sqlText)
//    //	{
//    //		DataTable result = new DataTable();
//    //		using (Database database = DatabaseFactory.CreateDatabase(this.dbcode))
//    //		{
//    //			database.ResetConnectionString(this.ConnString(dbName));
//    //			DbCommand sqlStringCommand = database.GetSqlStringCommand(sqlText);
//    //			using (IDataReader dataReader = database.ExecuteReader(sqlStringCommand))
//    //			{
//    //				bool flag = dataReader != null;
//    //				if (flag)
//    //				{
//    //					result = dataReader.GetSchemaTable();
//    //					for (int i = 0; i < dataReader.FieldCount; i++)
//    //					{
//    //					}
//    //					dataReader.Close();
//    //				}
//    //			}
//    //		}
//    //		return result;
//    //	}

//    //	// Token: 0x04000093 RID: 147
//    //	private string dbcode = "test";
//    //}
//}