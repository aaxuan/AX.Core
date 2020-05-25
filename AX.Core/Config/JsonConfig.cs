using AX.Core.CommonModel.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace AX.Core.Config
{
    public class JsonConfig
    {
        #region 属性

        private String _configFileNamePath { get; set; }

        private JObject _configJobject { get; set; }

        #endregion 属性

        /// <summary>
        /// 使用文件路径和实体类创建配置实例
        /// 无文件会根据类创建默认文件,若type传 null 值，则抛出异常
        /// </summary>
        /// <param name="configFileNamePath"></param>
        public JsonConfig(string configFileNamePath, Type configType = null)
        {
            _configFileNamePath = configFileNamePath;
            if (File.Exists(configFileNamePath) == false)
            {
                if (configType == null)
                { throw new FileNotFoundException("Json配置未找到配置文件", configFileNamePath); }
            }

            //读取配置文件
            var configFileText = File.ReadAllLines(_configFileNamePath);
            var jsonConfigText = new StringBuilder();
            //过滤注释行
            foreach (var item in configFileText)
            {
                if (item.Contains("//"))
                { continue; }
                jsonConfigText.Append(item);
            }
            if (jsonConfigText.Length <= 0)
            { throw new AXWarringMesssageException($"{configFileNamePath}配置文件无有效内容"); }

            _configJobject = JObject.Parse(jsonConfigText.ToString());
        }

        /// <summary>
        /// 获取特定类型值 没有则返回 defult(T)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            var result = default(T);
            result = _configJobject.Value<T>(key);
            return result;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, object value)
        {
            _configJobject[key] = JToken.FromObject(value);
        }
    }
}