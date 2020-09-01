using AX.Core.Cache;
using AX.Core.CommonModel.Exceptions;
using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Config
{
    public class ConfigFactory
    {
        private static readonly MemoryCache<IConfig> AllConfigDict = new MemoryCache<IConfig>("全局配置管理对象");

        public static IConfigT<T> CreateConfig<T>(string configName, string filepath) where T : class
        {
            configName.CheckIsNullOrWhiteSpace();
            if (AllConfigDict.ContainsKey(configName))
            { throw new AXWarringMesssageException($"{configName} 已存在该配置键值"); }
            var cache = new JsonConfig<T>(configName, filepath);
            AllConfigDict[configName] = cache;
            return cache;
        }

        public static IConfigT<T> GetCache<T>(string cacheName) where T : class
        {
            cacheName.CheckIsNullOrWhiteSpace();
            if (AllConfigDict.ContainsKey(cacheName))
            {
                return AllConfigDict[cacheName] as IConfigT<T>;
            }
            return null;
        }

        public static List<IConfig> GetAllCaCheList()
        {
            return AllConfigDict.AllToList();
        }
    }
}