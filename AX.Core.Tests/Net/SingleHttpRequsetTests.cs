using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace AX.Core.Net.Tests
{
    [TestClass()]
    public class SingleHttpRequsetTests
    {
        [TestMethod()]
        public void Test()
        {
            var baiduhtml = new SingleHttpRequset().Init(HttpMethod.GET, "https://www.baidu.com").GetStringResult();
            Assert.IsFalse(string.IsNullOrWhiteSpace(baiduhtml));
            Debug.Print(baiduhtml);
        }
         
    }
}