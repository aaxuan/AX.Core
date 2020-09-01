using AX.Core.Extension;
using AX.Core.Reflection;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace AX.Core.Config
{
    internal class JsonConfig<T> : IConfig<T> where T : class
    {
        public static T Current { get; set; }

        public JsonConfig(string name, string filepath)
        {
            filepath.CheckIsNullOrWhiteSpace();
            if (string.IsNullOrWhiteSpace(name))
            { Name = typeof(T).FullName; }
            Name = name;
            this.FilePath = filepath;
            Current = TypeManager.CreateInstance<T>();
        }

        #region 属性

        public string Name { get; private set; }

        public string FilePath { get; private set; }

        public String CaCheValueTypeName { get { return typeof(T).FullName; } }

        public DateTime? LoadTime { get; private set; }

        public DateTime? LastSaveTime { get; private set; }

        #endregion 属性

        public bool Load()
        {
            if (File.Exists(FilePath) == false)
            { throw new FileNotFoundException($"{Name} JSON配置类未找到配置文件 {FilePath}"); }
            var fileText = File.ReadAllLines(FilePath);
            var configText = new StringBuilder();

            foreach (var item in fileText)
            {
                //过滤注释行
                if (item.Contains("//")) { continue; }

                configText.Append(item);
            }
            Current = JObject.Parse(configText.ToString()) as T;
            return true;
        }

        public bool Save()
        {
            File.WriteAllText(FilePath, Current.ToString());
            return true;
        }
    }
}