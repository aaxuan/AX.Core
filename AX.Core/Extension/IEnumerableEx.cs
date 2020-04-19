using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace AX.Core.Extension
{
    public static class IEnumerableEx
    {
        /// <summary>
        /// 判断ICollection 是否有值 或 Null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 转化到DataTable
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">IEnumerable 对象</param>
        /// <returns>数据表格</returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            if (data == null)
            { throw new NullReferenceException(); }

            var result = new DataTable();
            var props = typeof(T).GetProperties();

            foreach (var item in props)
            {
                var displayName = item.GetCustomAttribute<DisplayNameAttribute>(false);
                if (displayName != null && !String.IsNullOrEmpty(displayName.DisplayName))
                { result.Columns.Add(displayName.DisplayName); continue; }

                var description = item.GetCustomAttribute<DescriptionAttribute>(false);
                if (description != null && !String.IsNullOrEmpty(description.Description))
                { result.Columns.Add(description.Description); continue; }

                result.Columns.Add(item.Name);
            }

            foreach (T item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                result.Rows.Add(values);
            }
            return result;
        }

        /// <summary>
        /// 转 HashSet<T>
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> data)
        {
            if (data == null)
            { throw new ArgumentNullException(); }

            var result = new HashSet<T>();

            foreach (T item in data)
            {
                result.Add(item);
            }

            return result;
        }
    }
}