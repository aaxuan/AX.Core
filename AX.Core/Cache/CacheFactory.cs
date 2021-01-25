using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;

namespace AX.Core.Cache
{
    public static class CacheFactory
    {
        private static readonly ConcurrentDictionary<string, IMemoryCache> AllCacheDict = new ConcurrentDictionary<string, IMemoryCache>();

        public static IMemoryCache CreateCache<T>(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName))
            { throw new ArgumentNullException(nameof(cacheName)); }

            if (AllCacheDict.ContainsKey(cacheName))
            { throw new ArgumentException($"{cacheName} 缓存已存在"); }

            var cache = new MemoryCache(new MemoryCacheOptions() { });
            AllCacheDict[cacheName] = cache;
            return cache;
        }

        public static IMemoryCache GetCache(string cacheName)
        {
            if (string.IsNullOrWhiteSpace(cacheName))
            { throw new ArgumentNullException(nameof(cacheName)); }

            if (AllCacheDict.ContainsKey(cacheName))
            {
                return AllCacheDict[cacheName];
            }
            return null;
        }
    }
}