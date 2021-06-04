using AX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace AX
{
    public static partial class Extention
    {
        public static bool IsNull(this object obj)
        {
            if (obj == null)
            { return true; }
            return false;
        }

        /// <summary>
        /// 快速序列化 复杂情况请勿使用
        /// 空引用返回 null
        /// 该方法尚未完善，可能会抛出异常
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FastToJson(this object obj)
        {
            if (obj.IsNull()) { return "null"; }
            if (obj.GetType().IsValueType) { return obj.ToString(); }
            StringBuilder result = new StringBuilder();
            result.Append("{");
            var propertyInfos = obj.GetType().GetProperties();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                result.Append("\"" + propertyInfos[i].Name + "\":\"" + propertyInfos[i].GetValue(obj).ToString() + "\"");
                if (i != propertyInfos.Length - 1) { result.Append(","); }
            }
            result.Append("}");
            return result.ToString();
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToXml<T>(this T obj)
        {
            var jsonStr = obj.ToJson();
            var xmlDoc = JsonConvert.DeserializeXmlNode(jsonStr);
            string xmlDocStr = xmlDoc.InnerXml;
            return xmlDocStr;
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, GlobalDefaultSetting.BindingFlags).GetValue(obj);
        }

        public static void SetPropertyValue(this object obj, string propertyName, object value)
        {
            obj.GetType().GetProperty(propertyName, GlobalDefaultSetting.BindingFlags).SetValue(obj, value);
        }

        public static object GetGetFieldValue(this object obj, string fieldName)
        {
            return obj.GetType().GetField(fieldName, GlobalDefaultSetting.BindingFlags).GetValue(obj);
        }

        public static void SetFieldValue(this object obj, string fieldName, object value)
        {
            obj.GetType().GetField(fieldName, GlobalDefaultSetting.BindingFlags).SetValue(obj, value);
        }

        public static MethodInfo GetMethod(this object obj, string methodName)
        {
            return obj.GetType().GetMethod(methodName, GlobalDefaultSetting.BindingFlags);
        }

        /// <summary>
        /// 使用 json 转换类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T ChangeTypeByJson<T>(this object obj)
        {
            return JsonConvert.DeserializeObject<T>(obj.ToJson());
        }

        /// <summary>
        /// 使用 Convert.ChangeType 转换类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object ChangeTypeByConvert(this object obj, Type targetType)
        {
            object resultObject;
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter newNullableConverter = new NullableConverter(targetType);
                resultObject = newNullableConverter.ConvertFrom(obj);
            }
            else
            {
                resultObject = Convert.ChangeType(obj, targetType);
            }
            return resultObject;
        }

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
            catch
            {
                throw;
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
    }
}