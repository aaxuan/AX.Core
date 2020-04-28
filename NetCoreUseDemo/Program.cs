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
}

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DB();
        db.UseConfig(new AX.Core.DataBase.Configs.MySqlDialectConfig());

        db.SetSchema<DemoTable>(true);
        var guid = db.NewGuId;
        var model = db.Insert<DemoTable>(new DemoTable() { Id = guid, CreateTime = DateTime.Now, isuse = true, money = 98.8M });
        model.CreateTime2 = DateTime.Now;
        model.money2 = 100.02M;
        db.Update<DemoTable>(model);
    }
}