using AX.Core.Extension;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace AX.Core.DataBaseSchema.Providers
{
    public class MySqlSchemaProvider : ISchemaProvider
    {
        public List<SchemaDB> LoadSchemaDBs(DbConnection dbConnection)
        {
            dbConnection.Open();
            var result = new List<SchemaDB>();
            var table = new DataTable();
            table.Load(dbConnection.ExecuteReader("SHOW DATABASES"));
            dbConnection.Open();
            foreach (DataRow row in table.Rows)
            {
                var resultItem = new SchemaDB();
                resultItem.CodeName = row[0].ToString();
                resultItem.Description = row[0].ToString();
                resultItem.DisplayName = row[0].ToString();
                resultItem.ConnectionString = dbConnection.ConnectionString;
                result.Add(resultItem);
            }
            return result;
        }

        public List<SchemaTable> LoadSchemaTable(SchemaDB schemaDB, DbConnection dbConnection)
        {
            schemaDB.CodeName.CheckIsNullOrWhiteSpace();
            // return $"SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{dbName}';";
            throw new NotSupportedException();

            //        public List<SchemaTable> LoadDBSchemaTables(string dbName)
            //        {
            //            var result = new List<SchemaTable>();
            //            var dbtabletb = GetDataTable(_dbConfig.GetLoadDBTableSql(dbName));
            //            foreach (DataRow row in dbtabletb.Rows)
            //            {
            //                var resultItem = new SchemaTable();
            //                resultItem.CodeName = row["TABLE_NAME"].ToString();
            //                resultItem.Description = row["TABLE_COMMENT"].ToString();
            //                resultItem.DisplayName = row["TABLE_NAME"].ToString();
            //                result.Add(resultItem);
            //            }
            //            return result;
            //        }
        }

        public List<SchemaColumn> LoadDBColmun(SchemaDB schemaDB, SchemaTable schemaTable, DbConnection dbConnection)
        {
            schemaDB.CodeName.CheckIsNullOrWhiteSpace();
            schemaTable.CodeName.CheckIsNullOrWhiteSpace();
            //return $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{dbName}' AND TABLE_NAME = '{tablename}';";
            throw new NotSupportedException();

            //        public List<SchemaColmun> LoadDBColmuns(string dbName, string tanbleName)
            //        {
            //            var result = new List<SchemaColmun>();
            //            var dbcolmuntb = GetDataTable(_dbConfig.GetLoadDBColmunSql(dbName, tanbleName));
            //            foreach (DataRow row in dbcolmuntb.Rows)
            //            {
            //                var resultItem = new SchemaColmun();
            //                resultItem.CodeName = row["COLUMN_NAME"].ToString();
            //                resultItem.DefaultValue = row["COLUMN_DEFAULT"].ToString();
            //                resultItem.CanNullable = row["IS_NULLABLE"].ToString() == "YES" ? true : false;
            //                resultItem.IsPrimaryKey = row["COLUMN_KEY"].ToString() == "PRI" ? true : false;
            //                resultItem.DisplayName = resultItem.Description = row["COLUMN_COMMENT"].ToString();
            //                resultItem.DBType = row["DATA_TYPE"].ToString();
            //                resultItem.Order = int.Parse(row["ORDINAL_POSITION"].ToString());
            //                result.Add(resultItem);
            //            }
            //            return result;
            //        }
        }
    }
}