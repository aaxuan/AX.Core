using AX.Core.Extension;
using System;

namespace AX.Core.Cache
{
    /// <summary>
    /// 缓存工厂 提供全局管理缓存的能力
    /// </summary>
    public static class CacheFactory
    {
        private static MemoryCache<Object> _allCacheDict;

        static CacheFactory()
        {
            _allCacheDict = new MemoryCache<Object>("全局缓存管理对象");
        }

        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="cacheName"></param>
        public static Object CreateCache(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            var cache = new MemoryCache<Object>(cacheName);
            _allCacheDict[cacheName] = cache;
            return cache;
        }

        public static Object GetCache(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (_allCacheDict.ContainsKey(cacheName))
            {
                return _allCacheDict[cacheName];
            }
            return null;
        }
    }
}