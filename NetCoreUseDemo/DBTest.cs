﻿using AX.Core.Business.DataModel;
using System;

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

        //private static DataRepository GetDataBase(SchemaDB schemaDB)
        //{
        //    return new DataRepository(new MySqlConnection(schemaDB.ConnectionString));
        //}

        public void Test()
        {
            var jsonconfig = new AX.Core.Config.TinyJsonConfig("jsonconfig.json");
            var b = jsonconfig.GetListValue<String>("testtext");
            jsonconfig.SetValue("aaaaaa", "qweqwe");
            jsonconfig.SetValue("bbb", "qweqwe");
            jsonconfig.SaveToFile();

            //DBFactory.SetDB(DBFactory.DefaultDBKey, jsonconfig.GetValue<string>("CompanyConnectionString"), DataBaseType.MySql);
            //DBFactory.GetDataRepositoryFunc = GetDataBase;

            //var db = DBFactory.GetDataRepository(DBFactory.DefaultDBKey);
            //var b = db.TestConnection();
            //var alldb = db.LoadDBSchemas();
            //var alltable = db.LoadDBSchemaTables(alldb.First().CodeName);
            //var allcolmun = db.LoadDBColmuns(alldb.First().CodeName, alltable.First().CodeName);

            //db.SetSchemaByModel<DemoTable>(true);
            //var model = db.Insert<DemoTable>(new DemoTable() { CreateTime = DateTime.Now, isuse = true, money = 98.8M });
            //model.CreateTime2 = DateTime.Now;
            //model.money2 = 100.02M;
            //db.Update<DemoTable>(model);
        }
    }
}