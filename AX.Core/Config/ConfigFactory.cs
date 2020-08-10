using AX.Core.Cache;
using AX.Core.CommonModel.Exceptions;
using AX.Core.Extension;
using System;

namespace AX.Core.Config
{
    public class ConfigFactory
    {
        private static readonly MemoryCache<TinyJsonConfig> _configCache;
        public static readonly String DefaultFileNamePath = "jsonconfig.json";

        static ConfigFactory()
        {
            _configCache = CacheFactory.CreateCache<TinyJsonConfig>("配置实例缓存") as MemoryCache<TinyJsonConfig>;
        }

        public static TinyJsonConfig CreateConfig(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            var config = new TinyJsonConfig(filePath);
            _configCache[filePath] = config;
            return config;
        }

        public static TinyJsonConfig GetDefaultConfig()
        {
            if (_configCache.ContainsKey(DefaultFileNamePath))
            {
                return _configCache[DefaultFileNamePath];
            }
            else
            { throw new AXWarringMesssageException($"[{_configCache.Name}] 缓存无 [{DefaultFileNamePath}] 值对象"); }
        }

        public static TinyJsonConfig GetConfig(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            if (_configCache.ContainsKey(filePath))
            {
                return _configCache[filePath];
            }
            return null;
        }
    }
}