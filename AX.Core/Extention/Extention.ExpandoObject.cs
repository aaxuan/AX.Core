using System;
using System.Collections.Generic;
using System.Dynamic;

namespace AX
{
    public static partial class Extention
    {
        public static void AddProperty(this ExpandoObject expandoObj, string propertyName, object value)
        {
            var dict = expandoObj as IDictionary<string, object>;
            if (dict.ContainsKey(propertyName)) { throw new Exception($"{propertyName} 属性已存在"); }
            dict.Add(propertyName, value);
        }

        public static void SetProperty(this ExpandoObject expandoObj, string propertyName, object value)
        {
            var dict = expandoObj as IDictionary<string, object>;
            if (!dict.ContainsKey(propertyName)) { dict.Add(propertyName, value); }
            dict[propertyName] = value;
        }

        public static object GetProperty(this ExpandoObject expandoObj, string propertyName)
        {
            var dict = expandoObj as IDictionary<string, object>;
            if (dict.ContainsKey(propertyName) == false) { throw new Exception($"{propertyName} 属性不存在"); }
            return dict[propertyName];
        }
    }
}