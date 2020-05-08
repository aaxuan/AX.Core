using AX.Core.CommonModel.Exceptions;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace AX.Core.Config
{
    public class JsonConfig : IDisposable
    {
        private String _configFileNamePath { get; set; }

        private JObject _configJobject { get; set; }

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

        #region IDisposable Support

        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。
                _configJobject = null;

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~JsonConfig()
        // {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}