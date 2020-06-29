using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Cache
{
    public static class CacheFactory
    {
        private static MemoryCache<ICaChe> _allCacheDict { get; set; } = new MemoryCache<ICaChe>("全局缓存管理对象");

        public static MemoryCache<T> CreateCache<T>(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            var cache = new MemoryCache<T>(cacheName);
            _allCacheDict[cacheName] = cache;
            return cache;
        }

        public static ICaChe GetCache(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (_allCacheDict.ContainsKey(cacheName))
            {
                return _allCacheDict[cacheName];
            }
            return null;
        }

        public static List<ICaChe> GetAllCaCheList()
        {
            return _allCacheDict.AllToList();
        }
    }
}