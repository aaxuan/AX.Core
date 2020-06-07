using System;

namespace AX.Core.DataBase.Schema.Attributes
{
    /// <summary>
    /// 完全忽略 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }

    /// <summary>
    /// 仅供查询 特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OnlySelectAttribute : Attribute
    {
    }

    ///// <summary>
    ///// 忽略插入 特性
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property)]
    //public class IgnoreInsertAttribute : Attribute
    //{
    //}

    ///// <summary>
    ///// 忽略更新 特性
    ///// </summary>
    //[AttributeUsage(AttributeTargets.Property)]
    //public class IgnoreUpdateAttribute : Attribute
    //{
    //}
}