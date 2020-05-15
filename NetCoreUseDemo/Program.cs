using AX.Core.Business.DataModel;
using NetCoreUseDemo;
using System;

namespace NetCoreUseDemo
{
    public class DemoTable
    {
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CreateTime2 { get; set; }

        public bool isuse { get; set; }

        public decimal money { get; set; }

        public decimal? money2 { get; set; }
    }

    public class User : BaseUser
    {
        public String LastLoginTime { get; set; }
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        //var db = new DB();
        //db.UseConfig(new AX.Core.DataBase.Configs.MySqlDialectConfig());

        //db.SetSchema<DemoTable>(true);
        //var guid = db.NewGuId;
        //var model = db.Insert<DemoTable>(new DemoTable() { Id = guid, CreateTime = DateTime.Now, isuse = true, money = 98.8M });
        //model.CreateTime2 = DateTime.Now;
        //model.money2 = 100.02M;
        //db.Update<DemoTable>(model);

        //AX.Core.Business.Managers.BaseUserManager userManager = new AX.Core.Business.Managers.BaseUserManager();
        //userManager.DB = db;
        //db.Insert<User>(new User() { NickName = "123qwe", LoginName = "asd", Password = "asd" });
        //userManager.New(new User() { NickName = "123qwe", LoginName = "asd", Password = "asd" });

        //var jsonconfi = new AX.Core.Config.JsonConfig("jsonConfig.json");
        //var x = jsonconfi.GetValue<string>("xxx");
        //var f = jsonconfi.GetValue<System.Collections.Generic.List<int>>("fff");
        //object c = jsonconfi.GetValue<object>("ccc");

        //百度坐标点 [113.312187194824,23.1383113861084]
        //高德坐标点 [113.305722,23.132239]
        116.473207,39.993202

        //我计算的高德坐标点 [113.305722,23.132239]

        var b = AX.Core.Helper.GPS.bd09_To_Gcj02(, );

        //
        //

        Console.WriteLine(b[0]);
        Console.WriteLine(b[1]);


        var m = 1 + 1;
    }
}