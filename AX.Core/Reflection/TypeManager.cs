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
    }
}