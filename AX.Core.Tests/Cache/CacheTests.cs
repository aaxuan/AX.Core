using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AX.Core.Cache.Tests
{
    [TestClass()]
    public class CacheTests
    {
        [TestMethod()]
        public void CacheFactoryTest()
        {
            CacheFactory.CreateCache<string>("测试缓存-1");
            var hashcode = CacheFactory.CreateCache<string>("测试缓存-2").GetHashCode();
            CacheFactory.CreateCache<string>("测试缓存-3");
            Assert.IsTrue(CacheFactory.GetCache("测试缓存-2").GetHashCode() == hashcode);
            var allCache = CacheFactory.GetAllCaCheList();
            Assert.IsTrue(allCache.Count == 3);
        }

        [TestMethod()]
        public void CacheTest()
        {
            ICaChe cache = CacheFactory.CreateCache<string>("测试缓存");
            Assert.IsTrue(cache.CaCheValueTypeName == typeof(string).FullName);
            Assert.IsTrue(cache.Count == 0);
            Assert.IsTrue(cache.Name == "测试缓存");

            Assert.IsTrue(cache is MemoryCache<string>);
            var memoryCache = (cache as MemoryCache<string>);
            memoryCache.Add("001", "testvalue-001");
            memoryCache.Add("002", "testvalue-002");
            Assert.IsTrue(memoryCache.AllToList().Count == 2);

            var dict = new Dictionary<string, string>();
            dict.Add("002", "testvalue-002-new");
            dict.Add("003", "testvalue-003");
            memoryCache.BatchAdd(dict);
            Assert.IsTrue(memoryCache.AllToList().Count == 3);

            memoryCache.Clear();
            Assert.IsTrue(memoryCache.AllToList().Count == 0);

            Assert.IsFalse(memoryCache.ContainsKey("001"));
            Assert.IsTrue(memoryCache.Add("001", "testvalue-001"));
            Assert.IsTrue(memoryCache.ContainsKey("001"));

            memoryCache.Add("002", "testvalue-002");
            Assert.AreEqual(memoryCache.Get("001"), "testvalue-001");

            Assert.IsTrue(memoryCache.GetList("001", "002").Count == 2);

            memoryCache.Remove("002");
            Assert.IsTrue(memoryCache.GetList("001", "002").Count == 2);
        }
    }
}