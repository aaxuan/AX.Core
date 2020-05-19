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
    public class MemoryCache<T>
    {
        /// <summary>
        /// 内存数据
        /// </summary>
        private ConcurrentDictionary<string, T> _dict { get; set; } = new ConcurrentDictionary<string, T>();

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

        /// <summary>
        /// 缓存名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 缓存数量
        /// </summary>
        public int Count { get { return _dict.Count; } }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

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
                return _dict[key];
            }

            //实现索引器的set方法
            set
            {
                _dict[key] = value;
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
            _dict[key] = value;
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
            _dict.Clear();
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            if (_dict.ContainsKey(key))
            {
                return _dict[key];
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
            _dict.TryRemove(key, out obj);
            return true;
        }

        /// <summary>
        /// 判断缓存存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return _dict.ContainsKey(key);
        }

        /// <summary>
        /// 获取全部缓存值
        /// </summary>
        /// <returns></returns>
        public List<T> AllToList()
        {
            return _dict.Values.ToList();
        }
    }
}