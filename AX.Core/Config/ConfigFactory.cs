using AX.Core.Cache;
using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Config
{
    public class ConfigFactory
    {
        private static readonly MemoryCache<JsonConfig> _configCache;

        static ConfigFactory()
        {
            _configCache = CacheFactory.CreateCache<JsonConfig>("配置实例缓存") as MemoryCache<JsonConfig>;
        }

        public static JsonConfig CreateConfig(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            var config = new JsonConfig(filePath);
            _configCache[filePath] = config;
            return config;
        }

        public static JsonConfig GetDefaultConfig()
        {
            if (_configCache.ContainsKey(JsonConfig.DefaultFileNamePath))
            {
                return _configCache[JsonConfig.DefaultFileNamePath];
            }
            return null;
        }

        public static JsonConfig GetConfig(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            if (_configCache.ContainsKey(filePath))
            {
                return _configCache[filePath];
            }
            return null;
        }

        public static List<JsonConfig> GetCaCheList()
        {
            return _configCache.AllToList();
        }
    }
}