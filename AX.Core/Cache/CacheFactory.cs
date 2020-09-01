using AX.Core.CommonModel.Exceptions;
using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Cache
{
    public static class CacheFactory
    {
        private static readonly MemoryCache<ICaChe> AllCacheDict = new MemoryCache<ICaChe>("全局缓存管理对象");

        public static MemoryCache<T> CreateCache<T>(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (AllCacheDict.ContainsKey(cacheName))
            { throw new AXWarringMesssageException($"{cacheName} 已存在该缓存键值"); }
            var cache = new MemoryCache<T>(cacheName);
            AllCacheDict[cacheName] = cache;
            return cache;
        }

        public static ICaChe GetCache(string cacheName)
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (AllCacheDict.ContainsKey(cacheName))
            {
                return AllCacheDict[cacheName];
            }
            return null;
        }

        public static List<ICaChe> GetAllCaCheList()
        {
            return AllCacheDict.AllToList();
        }
    }
}