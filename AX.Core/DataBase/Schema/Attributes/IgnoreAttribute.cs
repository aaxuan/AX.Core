using System;

namespace AX.Core.DataBase.Schema.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class OnlySelectAttribute : Attribute
    {
    }

    //[AttributeUsage(AttributeTargets.Property)]
    //public class IgnoreInsertAttribute : Attribute
    //{
    //}

    //[AttributeUsage(AttributeTargets.Property)]
    //public class IgnoreUpdateAttribute : Attribute
    //{
    //}
}