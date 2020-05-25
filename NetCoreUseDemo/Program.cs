//using AX.Core.Business.DataModel;

namespace NetCoreUseDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new LogTest().Test();

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
            //116.473207,39.993202

            //我计算的高德坐标点 [113.305722,23.132239]

            //var b = AX.Core.Helper.GPS.bd09_To_Gcj02(, );

            //
            //

            //Console.WriteLine(b[0]);
            //Console.WriteLine(b[1]);

            //server=localhost;userid=root;pwd=123qwe;database=test;sslmode=none;

            var m = 1 + 1;
        }
    }
}