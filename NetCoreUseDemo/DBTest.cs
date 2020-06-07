using AX.Core.Business.DataModel;
using AX.Core.DataBase;
using MySql.Data.MySqlClient;
using System;
using static AX.Core.DataBase.DBFactory;

namespace NetCoreUseDemo
{
    public class DBTest
    {
        public class DemoTable
        {
            public string Id { get; set; }

            public DateTime CreateTime { get; set; }

            public DateTime? CreateTime2 { get; set; }

            public bool isuse { get; set; }

            public decimal money { get; set; }

            [AX.Core.DataBase.Schema.Attributes.Ignore]
            public decimal? money2 { get; set; }
        }

        public class User : Base_User
        {
            public String LastLoginTime { get; set; }
        }

        private static DataRepository GetDataBase(AX.Core.DataBase.Schema.SchemaDB schemaDB)
        {
            return new DataRepository(new MySqlConnection(schemaDB.ConnectionString));
        }

        public void Test()
        {
            var jsonconfig = new AX.Core.Config.JsonConfig("jsonConfig.json");
            DBFactory.SetDB(DBFactory.DefaultDBKey, jsonconfig.GetValue<string>("HomeConnectionString"), DataBaseType.MySql);
            DBFactory.GetDataRepositoryFunc = GetDataBase;
            var db = DBFactory.GetDataRepository(DBFactory.DefaultDBKey);

            db.SetSchema<DemoTable>(true);
            var model = db.Insert<DemoTable>(new DemoTable() { CreateTime = DateTime.Now, isuse = true, money = 98.8M });
            model.CreateTime2 = DateTime.Now;
            model.money2 = 100.02M;
            db.Update<DemoTable>(model);
        }
    }
}