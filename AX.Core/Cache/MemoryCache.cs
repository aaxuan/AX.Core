using AX.Core.Extension;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Cache
{
    /// <summary>
    /// 内存缓存实现类
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    public class MemoryCache<T>
    {
        private ConcurrentDictionary<string, T> _dict { get; set; } = new ConcurrentDictionary<string, T>();

        public MemoryCache()
        { }

        public MemoryCache(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Name = typeof(T).FullName;
            }
            Name = name;
        }

        public string Name { get; set; }

        public int Count { get { return _dict.Count; } }

        public bool Add(string key, T value)
        {
            key.CheckIsNullOrWhiteSpace();
            _dict[key] = value;
            return true;
        }

        public bool BatchAdd(Dictionary<string, T> data)
        {
            foreach (var item in data)
            {
                Add(item.Key, item.Value);
            }
            return true;
        }

        public bool Clear()
        {
            _dict.Clear();
            return true;
        }

        public T Get(string key)
        {
            if (_dict.ContainsKey(key))
            {
                return _dict[key];
            }
            return default(T);
        }

        public List<T> GetList(params string[] keys)
        {
            var result = new List<T>();
            foreach (var key in keys)
            {
                result.Add(Get(key));
            }
            return result;
        }

        public bool Remove(string key)
        {
            var obj = default(T);
            _dict.TryRemove(key, out obj);
            return true;
        }

        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        public List<T> ToList()
        {
            return _dict.Values.ToList();
        }
    }
}