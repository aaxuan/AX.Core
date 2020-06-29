using AX.Core.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Cache
{
    public class MemoryCache<T> : ICaChe
    {
        private ConcurrentDictionary<string, T> _dataDict { get; set; } = new ConcurrentDictionary<string, T>();

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

        public int Count { get { return _dataDict.Count; } }

        public DateTime CreateTime { get; private set; }

        public String CaCheValueTypeName { get { return typeof(T).FullName; } }

        #endregion 属性

        public T this[string key]
        {
            //实现索引器的get方法
            get
            {
                return _dataDict[key];
            }

            //实现索引器的set方法
            set
            {
                _dataDict[key] = value;
            }
        }

        public bool Add(string key, T value)
        {
            key.CheckIsNullOrWhiteSpace();
            _dataDict[key] = value;
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
            _dataDict.Clear();
            return true;
        }

        public T Get(string key)
        {
            if (_dataDict.ContainsKey(key))
            {
                return _dataDict[key];
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
            _dataDict.TryRemove(key, out obj);
            return true;
        }

        public bool ContainsKey(string key)
        {
            return _dataDict.ContainsKey(key);
        }

        public List<T> AllToList()
        {
            return _dataDict.Values.ToList();
        }
    }
}