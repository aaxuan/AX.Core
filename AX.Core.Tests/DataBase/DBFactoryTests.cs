using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.DataBase.Tests
{
    [TestClass()]
    public class DBFactoryTests
    {
        [TestMethod()]
        public void DBFactoryGetDataRepositoryTests()
        {
            //mysql
            var db = DBFactory.GetDataRepository(new MySql.Data.MySqlClient.MySqlConnection("server=localhost;userid=root;pwd=;database=test;sslmode=none;"));
            Assert.IsTrue(db.TestConnection());
            var type = DBFactory.GetDataBaseType(db.Connection);
            DBFactory.GetAdapter(type);
        }
    }
}