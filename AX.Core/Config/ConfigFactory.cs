using AX.Core.Cache;
using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Config
{
    /// <summary>
    /// 配置工厂 提供全局管理配置的能力
    /// </summary>
    internal class ConfigFactory
    {
        private static readonly MemoryCache<JsonConfig> _configCache;

        static ConfigFactory()
        {
            _configCache = CacheFactory.CreateCache<JsonConfig>("配置实例缓存") as MemoryCache<JsonConfig>;
        }

        /// <summary>
        /// 创建并注册配置对象
        /// </summary>
        /// <param name="cacheName"></param>
        public static JsonConfig CreateConfig(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            var config = new JsonConfig(filePath);
            _configCache[filePath] = config;
            return config;
        }

        /// <summary>
        /// 获取配置对象
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public static JsonConfig GetCache(string filePath)
        {
            filePath.CheckIsNullOrWhiteSpace();
            if (_configCache.ContainsKey(filePath))
            {
                return _configCache[filePath];
            }
            return null;
        }

        /// <summary>
        /// 获取全局所有缓存情况
        /// </summary>
        /// <returns></returns>
        public static List<JsonConfig> GetCaCheList()
        {
            return _configCache.AllToList();
        }
    }
}