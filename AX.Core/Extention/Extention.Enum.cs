using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AX
{
    public static partial class Extention
    {
        /// <summary>
        /// 获取枚举字段的注释 不存在返回 null
        /// System.ComponentModel.DescriptionAttribute
        /// </summary>
        public static String GetDescription(this Enum value)
        {
            if (value == null) { throw new ArgumentNullException(); }

            var EnumType = value.GetType();
            var EnumItem = EnumType.GetField(value.ToString(), BindingFlags.Public | BindingFlags.Static);

            if (EnumItem == null)
            { return null; }

            var att = EnumItem.GetCustomAttribute<DescriptionAttribute>(false);
            if (att != null && !String.IsNullOrEmpty(att.Description))
            { return att.Description; }

            return null;
        }

        /// <summary>
        /// 获取枚举类型的所有字段注释
        /// </summary>
        public static Dictionary<Enum, String> GetDescriptions(this Enum value)
        {
            var dict = new Dictionary<Enum, String>();
            foreach (var item in GetDescriptions(value.GetType()))
            {
                dict.Add((Enum)(Object)item.Key, item.Value);
            }
            return dict;
        }

        ///// <summary>
        ///// 获取枚举类型的所有字段注释
        ///// </summary>
        //public static List<KeyValueModel> GetIdValue(Type enumType)
        //{
        //    var result = new List<KeyValueModel>();
        //    foreach (var item in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        //    {
        //        if (!item.IsStatic)
        //        { continue; }

        //        var value = Convert.ToInt32(item.GetValue(null));

        //        var des = item.Name;

        //        var dna = item.GetCustomAttribute<DisplayNameAttribute>(false);
        //        if (dna != null && !String.IsNullOrEmpty(dna.DisplayName))
        //        { des = dna.DisplayName; }

        //        var att = item.GetCustomAttribute<DescriptionAttribute>(false);
        //        if (att != null && !String.IsNullOrEmpty(att.Description))
        //        { des = att.Description; }

        //        result.Add(new KeyValueModel() { Key = value.ToString(), Value = des, Description = des });
        //    }
        //    return result;
        //}

        /// <summary>
        /// 检查枚举的值是否在枚举范围内
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValid(this Enum value)
        {
            return Enum.IsDefined(value.GetType(), value);
        }

        /// <summary>
        /// 获取枚举类型的所有字段注释
        /// System.ComponentModel.DescriptionAttribute 优先
        /// System.ComponentModel.DisplayNameAttribute
        /// </summary>
        private static Dictionary<Int32, String> GetDescriptions(Type enumType)
        {
            var dic = new Dictionary<Int32, String>();
            foreach (var item in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (!item.IsStatic)
                { continue; }

                var value = Convert.ToInt32(item.GetValue(null));

                var des = item.Name;

                var dna = item.GetCustomAttribute<DisplayNameAttribute>(false);
                if (dna != null && !String.IsNullOrEmpty(dna.DisplayName))
                { des = dna.DisplayName; }

                var att = item.GetCustomAttribute<DescriptionAttribute>(false);
                if (att != null && !String.IsNullOrEmpty(att.Description))
                { des = att.Description; }

                dic[value] = des;
            }

            return dic;
        }
    }
}