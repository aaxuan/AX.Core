using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AX.Core.Reflection
{
    public class TypeManager
    {
        /// <summary>
        /// 获取使用某特性全部类
        /// </summary>
        /// <returns></returns>
        public List<Type> GetAttributeAllType(Type attributeType)
        {
            Assembly asm = Assembly.GetAssembly(attributeType);
            Type[] types = asm.GetExportedTypes();

            Func<Attribute[], bool> IsMyAttribute = o =>
            {
                foreach (Attribute a in o)
                {
                    if (a.GetType() == attributeType)
                    { return true; }
                }
                return false;
            };
            var result = types.Where(p => IsMyAttribute(System.Attribute.GetCustomAttributes(p, true))).ToList();
            return result;
        }

        /// <summary>
        /// 创建类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 获取类型属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo[] GetProperties<T>()
        {
            return typeof(T).GetProperties();
        }

        /// <summary>
        /// 获取当前应用域全部类
        /// </summary>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static Type[] GetAllType()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
            return types;
        }
    }
}