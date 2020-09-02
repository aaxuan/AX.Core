using AX.Core.DataBase.Schema;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using static AX.Core.DataBase.DBFactory;

namespace AX.Core.DataBase.Tests
{
    //public class DemoTable
    //{
    //    public string Id { get; set; }

    //    public int Count { get; set; }

    //    public int? NullCount { get; set; }

    //    public DateTime CreateTime { get; set; }

    //    public DateTime? NullCreateTime { get; set; }

    //    public bool Isuse { get; set; }

    //    public decimal Money { get; set; }

    //    public decimal? NullMoney { get; set; }

    //    [Schema.Attributes.Ignore]
    //    public string filfield { get; set; }
    //}

    //[TestClass()]
    //public class DataRepositoryTests
    //{
    //    private static DataRepository GetDataBase(SchemaDB schemaDB)
    //    {
    //        return new DataRepository(new System.Data.SQLite.SQLiteConnection(schemaDB.ConnectionString));
    //    }

    //    [TestMethod()]
    //    public void DataRepositoryTest()
    //    {
    //        DBFactory.SetDB(DBFactory.DefaultDBKey, "Data Source=test.db", DataBaseType.SqlLite);
    //        DBFactory.GetDataRepositoryFunc = GetDataBase;

    //        var db = DBFactory.GetDataRepository(DBFactory.DefaultDBKey);
    //        Assert.IsTrue(db.TestConnection());
    //        db.SetSchemaByModel<DemoTable>(true);

    //        //var alldb = db.LoadDBSchemas();
    //        //var alltable = db.LoadDBSchemaTables(alldb.First().CodeName);
    //        //var allcolmun = db.LoadDBColmuns(alldb.First().CodeName, alltable.First().CodeName);
    //        Debug.Print(db.Delete<DemoTable>("WHERE 1 = 1", null).ToString());
    //        Assert.AreEqual(db.GetCount<DemoTable>(), 0); 

    //        var model = db.Insert(new DemoTable() { CreateTime = DateTime.Now, Isuse = true, Money = 98.8M, Count = 50 });
    //        Assert.IsNotNull(model.Id);
    //        model.NullCreateTime = DateTime.Now;
    //        model.NullMoney = 100.02M;
    //        db.Update<DemoTable>(model);
    //        var alldata = db.GetList<DemoTable>();
    //    }
    //}
}