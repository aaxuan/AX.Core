using AX.Core.Cache;
using AX.Core.CommonModel.Exceptions;
using AX.Core.Extension;
using System.Collections.Generic;

namespace AX.Core.Config
{
    public static class ConfigFactory
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

        public static IConfigT<T> GetConfig<T>(string configName) where T : class
        {
            configName.CheckIsNullOrWhiteSpace();
            if (AllConfigDict.ContainsKey(configName))
            {
                return AllConfigDict[configName] as IConfigT<T>;
            }
            return null;
        }

        public static List<IConfig> GetAllconfigList()
        {
            return AllConfigDict.AllToList();
        }

        public static bool Clear()
        {
            return AllConfigDict.Clear();
        }
    }
}