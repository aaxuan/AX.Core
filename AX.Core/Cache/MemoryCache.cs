using AX.Core.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Cache
{
    public class MemoryCache<T> : ICaChe
    {
        private readonly ConcurrentDictionary<string, T> Dict = new ConcurrentDictionary<string, T>();

        public MemoryCache(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Name = typeof(T).FullName;
            }
            Name = name;
            this.CreateTime = DateTime.Now;
        }

        #region 属性

        public string Name { get; private set; }

        public int Count { get { return Dict.Count; } }

        public DateTime CreateTime { get; private set; }

        public String CaCheValueTypeName { get { return typeof(T).FullName; } }

        #endregion 属性

        public T this[string key]
        {
            get
            {
                return Dict[key];
            }
            set
            {
                Dict[key] = value;
            }
        }

        public bool Add(string key, T value)
        {
            key.CheckIsNullOrWhiteSpace();
            Dict[key] = value;
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
            Dict.Clear();
            return true;
        }

        public T Get(string key)
        {
            if (Dict.ContainsKey(key))
            {
                return Dict[key];
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
            Dict.TryRemove(key, out obj);
            return true;
        }

        public bool ContainsKey(string key)
        {
            return Dict.ContainsKey(key);
        }

        public List<T> AllToList()
        {
            return Dict.Values.ToList();
        }
    }
}