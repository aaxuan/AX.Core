using AX.Core.CommonModel.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AX.Core.Config
{
    public class TinyJsonConfig
    {
        public String ConfigFileNamePath { get; private set; } = ConfigFactory.DefaultFileNamePath;

        private JObject ConfigJobject { get; set; }

        public TinyJsonConfig(string fileNamePath = null)
        {
            if (string.IsNullOrWhiteSpace(fileNamePath) == false)
            { ConfigFileNamePath = fileNamePath; }

            if (File.Exists(ConfigFileNamePath) == false)
            { throw new FileNotFoundException("Json配置类未找到配置文件", ConfigFileNamePath); }

            //读取配置文件
            var fileText = File.ReadAllLines(ConfigFileNamePath);
            var jsonConfigText = new StringBuilder();

            //过滤注释行
            foreach (var item in fileText)
            {
                if (item.Contains("//"))
                { continue; }
                jsonConfigText.Append(item);
            }
            if (jsonConfigText.Length <= 0)
            { throw new AXWarringMesssageException($"{fileNamePath} Json配置文件无有效内容"); }
            ConfigJobject = JObject.Parse(jsonConfigText.ToString());
        }

        public T GetValue<T>(string key)
        {
            var result = default(T);
            result = ConfigJobject.Value<T>(key);
            return result;
        }

        public List<T> GetListValue<T>(string key)
        {
            var result = default(List<T>);
            var jarray = JArray.Parse(ConfigJobject.GetValue(key).ToString());
            result = jarray.ToObject<List<T>>();
            return result;
        }

        public void SetValue(string key, object value)
        {
            if (ConfigJobject.ContainsKey(key))
            { ConfigJobject[key] = JToken.FromObject(value); }
            else
            { ConfigJobject.Add(new JProperty(key, value)); }
        }

        public void SaveToFile()
        {
            File.WriteAllText(ConfigFileNamePath, ConfigJobject.ToString());
        }
    }
}