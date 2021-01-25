using AX.Core;
using Newtonsoft.Json;
using System;
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
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string FastToJson(this object obj)
        {
            if (obj.IsNull()) { return "null"; }
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
    }
}