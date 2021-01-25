using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AX.Core.Reflection
{
    public class AssemblyManager
    {
        public static readonly List<Assembly> AllAssembly;
        public static readonly List<Type> AllTypes = new List<Type>();

        static AssemblyManager()
        {
            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AllAssembly = Directory.GetFiles(rootPath, "*.dll")
                .Select(x => Assembly.LoadFrom(x))
                .Where(x => !x.IsDynamic)
                .ToList();

            AllAssembly.ForEach(aAssembly =>
            {
                AllTypes.AddRange(aAssembly.GetTypes());
            });
        }
    }
}