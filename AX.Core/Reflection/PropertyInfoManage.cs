using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AX.Core.Reflection
{
    public class PropertyInfoManage
    {
        /// <summary>
        /// 获取 StringLengthAttribute 特性值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static int GetMaxStringLength(PropertyInfo field)
        {
            var att = field.GetCustomAttribute<StringLengthAttribute>(false);
            if (att != null)
            { return att.MaximumLength; }
            return 0;
        }

        /// <summary>
        /// 获取 Description 特性值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetDescription(PropertyInfo field)
        {
            var att = field.GetCustomAttribute<DescriptionAttribute>(false);
            if (att != null && !String.IsNullOrEmpty(att.Description))
            { return att.Description; }
            return string.Empty;
        }

        /// <summary>
        /// 获取 DisplayName 特性值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetDisplayName(PropertyInfo field)
        {
            var att = field.GetCustomAttribute<DisplayNameAttribute>(false);
            if (att != null && !String.IsNullOrEmpty(att.DisplayName))
            { return att.DisplayName; }
            return string.Empty;
        }

        /// <summary>
        /// 获取 Description 特性值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetDescription(FieldInfo field)
        {
            var att = field.GetCustomAttribute<DescriptionAttribute>(false);
            if (att != null && !String.IsNullOrEmpty(att.Description))
            { return att.Description; }
            return string.Empty;
        }

        /// <summary>
        /// 获取 DisplayName 特性值
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetDisplayName(FieldInfo field)
        {
            var att = field.GetCustomAttribute<DisplayNameAttribute>(false);
            if (att != null && !String.IsNullOrEmpty(att.DisplayName))
            { return att.DisplayName; }
            return string.Empty;
        }
    }
}