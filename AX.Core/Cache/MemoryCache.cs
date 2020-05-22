using AX.Core.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AX.Core.Cache
{
    /// <summary>
    /// 内存缓存实现类
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    public class MemoryCache<T> : ICaChe
    {
        /// <summary>
        /// 内存数据
        /// </summary>
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

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(string key, T value)
        {
            key.CheckIsNullOrWhiteSpace();
            _dataDict[key] = value;
            return true;
        }

        /// <summary>
        /// 批量添加缓存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool BatchAdd(Dictionary<string, T> data)
        {
            foreach (var item in data)
            {
                Add(item.Key, item.Value);
            }
            return true;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            _dataDict.Clear();
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            if (_dataDict.ContainsKey(key))
            {
                return _dataDict[key];
            }
            return default(T);
        }

        /// <summary>
        /// 获取多个缓存
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<T> GetList(params string[] keys)
        {
            var result = new List<T>();
            foreach (var key in keys)
            {
                result.Add(Get(key));
            }
            return result;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            var obj = default(T);
            _dataDict.TryRemove(key, out obj);
            return true;
        }

        /// <summary>
        /// 判断缓存存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _dataDict.ContainsKey(key);
        }

        /// <summary>
        /// 获取全部缓存值
        /// </summary>
        /// <returns></returns>
        public List<T> AllToList()
        {
            return _dataDict.Values.ToList();
        }
    }
}