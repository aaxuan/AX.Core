using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Cache
{
    /// <summary>
    /// 缓存工厂 提供全局管理缓存的能力
    /// </summary>
    public static class CacheFactory
    {
        /// <summary>
        /// 所有缓存
        /// </summary>
        private static MemoryCache<ICaChe> _allCacheDict { get; set; } = new MemoryCache<ICaChe>("全局缓存管理对象");

        /// <summary>
        /// 创建并注册缓存对象
        /// </summary>
        /// <param name="cacheName"></param>
        public static MemoryCache<T> CreateCache<T>(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            var cache = new MemoryCache<T>(cacheName);
            _allCacheDict[cacheName] = cache;
            return cache;
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public static ICaChe GetCache(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (_allCacheDict.ContainsKey(cacheName))
            {
                return _allCacheDict[cacheName];
            }
            return null;
        }

        /// <summary>
        /// 获取全局所有缓存情况
        /// </summary>
        /// <returns></returns>
        public static List<ICaChe> GetCaCheList()
        {
            return _allCacheDict.AllToList();
        }
    }
}