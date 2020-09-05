using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AX.Core.Config.Tests
{
    [TestClass()]
    public class ConfigTests
    {
        public class ConfigObject
        {
            public string ConfigName { get; set; } = "测试配置类 默认值-写入值";
            public string ConfigCode { get; set; } = "EA123636";
            public int ConfigInt { get; set; } = 654;
            public decimal ConfigDecimal { get; set; } = 89.965M;
            public bool ConfigBool { get; set; } = false;
        }

        [TestMethod()]
        public void ConfigFactoryTest()
        {
            ConfigFactory.CreateConfig<ConfigObject>("测试配置类1", System.Environment.CurrentDirectory + "/测试配置类1.json");
            var hashcode = ConfigFactory.CreateConfig<ConfigObject>("测试配置类2", System.Environment.CurrentDirectory + "/测试配置类2.json").GetHashCode();
            ConfigFactory.CreateConfig<ConfigObject>("测试配置类3", System.Environment.CurrentDirectory + "/测试配置类3.json");
            Assert.IsTrue(ConfigFactory.GetConfig<ConfigObject>("测试配置类2").GetHashCode() == hashcode);
            var allCache = ConfigFactory.GetAllconfigList();
            Assert.IsTrue(allCache.Count == 3);
            ConfigFactory.Clear();
        }

        [TestMethod()]
        public void ConfigTest()
        {
            ConfigFactory.CreateConfig<ConfigObject>("测试配置类1", System.Environment.CurrentDirectory + "/测试配置类1.json");
            var config = ConfigFactory.GetConfig<ConfigObject>("测试配置类1");
            //Assert.IsTrue(config.Save());
            Assert.IsTrue(config.Load());
            System.Diagnostics.Debug.WriteLine(config.GetCurrentConfig().ConfigName);
        }
    }
}