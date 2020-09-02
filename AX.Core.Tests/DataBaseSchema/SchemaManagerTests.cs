using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.DataBaseSchema.Tests
{
    [TestClass()]
    public class SchemaManagerTests
    {
        [TestMethod()]
        public void GetSchemaProviderTest()
        {
            //mysql
            var provider = SchemaManager.GetSchemaProvider(DataBase.DataBaseType.MySql);
            var conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;userid=root;pwd=;database=test;sslmode=none;");
            var dbs = provider.LoadSchemaDBs(conn);
            foreach (var db in dbs)
            {
                db.Tables = provider.LoadSchemaTable(db, conn);
                foreach (var table in db.Tables)
                {
                    table.Colmuns = provider.LoadDBColmun(db, table, conn);
                }
            }
        }
    }
}