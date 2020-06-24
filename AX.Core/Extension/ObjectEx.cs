using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AX.Core.Extension
{
    public static class ObjectEx
    {
        /// <summary>
        /// 检查是否空对象 是则抛出 NullReferenceException 异常
        /// </summary>
        /// <param name="obj">对象</param>
        public static void CheckIsNull(this object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 映射同名值
        /// </summary>
        /// <typeparam name="TSourse">原类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="objSourse">原类型对象</param>
        /// <param name="objTarget">目标类型对象</param>
        /// <returns>目标类型对象</returns>
        public static TTarget MapTo<TSourse, TTarget>(this TSourse objSourse, TTarget objTarget)
        {
            try
            {
                var sourseTypePropertyInfo = typeof(TSourse).GetProperties();
                var targetTypePropertyInfo = typeof(TTarget).GetProperties();

                foreach (var sp in sourseTypePropertyInfo)
                {
                    foreach (var tp in targetTypePropertyInfo)
                    {
                        if (tp.Name == sp.Name && tp.PropertyType == sp.PropertyType)
                        {
                            tp.SetValue(objTarget, sp.GetValue(objSourse));
                        }
                    }
                }
                return objTarget;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 转换为字符串字典
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字典</returns>
        public static IDictionary<string, string> ToDict(this Object obj)
        {
            obj.CheckIsNull();

            var result = new Dictionary<string, string>();
            var props = obj.GetType().GetProperties();
            //取配置值
            foreach (var item in props)
            {
                var key = item.Name;
                var value = item.GetValue(obj);
                result[key] = value == null ? string.Empty : value.ToString();
            }
            return result;
        }

        public static string ToJson(this Object obj)
        { return JsonConvert.SerializeObject(obj); }


    }
}