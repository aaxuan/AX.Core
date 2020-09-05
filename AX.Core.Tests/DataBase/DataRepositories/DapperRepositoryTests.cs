﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace AX.Core.DataBase.DataRepositories.Tests
{
    public class DemoTable
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public int? NullCount { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? NullCreateTime { get; set; }
        public bool Isuse { get; set; }
        public decimal Money { get; set; }
        public decimal? NullMoney { get; set; }
        public string Filfield { get; set; }
    }

    [TestClass()]
    public class DapperRepositoryTests
    {
        [TestMethod()]
        public void DapperRepositoryTest()
        {
            var db = DBFactory.GetDataRepository(new MySql.Data.MySqlClient.MySqlConnection("server=localhost;userid=root;pwd=;database=test;sslmode=none;"));
            Assert.IsTrue(db.TestConnection());
            var sql1 = db.GetCreateTableSql<DemoTable>();
            Debug.Print(sql1);
            var sql2 = db.UpdateSchema<DemoTable>(true);
            Debug.Print(sql2);

            Debug.Print(db.DeleteTable<DemoTable>().ToString());

            Assert.IsTrue(db.GetCount<DemoTable>() == 0);
            var model = db.Insert(new DemoTable() { Id = "000001", CreateTime = DateTime.Now, Isuse = true, Money = 98.8M, Count = new Random().Next(5, 8000) });
            Assert.IsNotNull(model.Id);
            model.NullCreateTime = DateTime.Now;
            model.NullMoney = 100.02M;
            db.Update<DemoTable>(model);
            var alldata = db.GetAll<DemoTable>().Count;
            Assert.IsTrue(db.GetCount<DemoTable>() == 1);
        }

        //[TestMethod()]
        //public void AbortTransactionTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void CompleteTransactionTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void BeginTransactionTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void ExecuteNonQueryTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void ExecuteScalarTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void SaveTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void BatchInsertTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void DeleteTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void UpdateTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void IsExistsTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void IsExistsTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void GetCountTest1()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void FirstOrDefaultTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void FirstOrDefaultByIdTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void SingleOrDefaultTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void SingleOrDefaultByIdTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void GetListTest()
        //{
        //    throw new NotImplementedException();
        //}

        //[TestMethod()]
        //public void GetDataTableTest()
        //{
        //    throw new NotImplementedException();
        //}
    }
}