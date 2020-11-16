using AX.Core.Extension;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Text;

namespace AX.Core.Net
{
    public enum HttpMethod
    {
        GET,
        POST,
    }

    public class SingleHttpRequset
    {
        private HttpWebRequest InnerHttpWebRequest;
        public CookieContainer CookieContainer = new CookieContainer();
        public Encoding Encoding = AxCoreGlobalSettings.Encodeing;

        public SingleHttpRequset Init(HttpMethod httpMethod, string url)
        {
            url.CheckIsNullOrWhiteSpace();
            //不包含协议头则自动追加 HTTP 协议头
            if (url.StartsWith("http://", System.StringComparison.CurrentCultureIgnoreCase) == false && url.StartsWith("https://", System.StringComparison.CurrentCultureIgnoreCase) == false)
            { url = string.Concat("http://", url); }
            InnerHttpWebRequest = null;
            InnerHttpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            InnerHttpWebRequest.Method = httpMethod.ToString();
            InnerHttpWebRequest.CookieContainer = CookieContainer;
            return this;
        }

        #region 设置属性

        public SingleHttpRequset SetUserAgent(string userAgent)
        {
            InnerHttpWebRequest.UserAgent = userAgent;
            return this;
        }

        public SingleHttpRequset SetHeader(string name, string value)
        {
            InnerHttpWebRequest.Headers.Add(name, value);
            return this;
        }

        public SingleHttpRequset SetReferer(string referer)
        {
            InnerHttpWebRequest.Referer = referer;
            return this;
        }

        #endregion 设置属性

        #region 设置参数

        // content-type 常见的主要有如下3种：
        //application/x-www-form-urlencoded
        //multipart/form-data
        //application/json

        public SingleHttpRequset SetJsonData<T>(T data)
        {
            InnerHttpWebRequest.ContentType = "application/json;charset=UTF-8";
            var value = JsonConvert.SerializeObject(data);
            var valueBytes = Encoding.GetBytes(value);
            InnerHttpWebRequest.ContentLength = valueBytes.Length;
            Stream requsetStream = InnerHttpWebRequest.GetRequestStream();
            requsetStream.Write(valueBytes, 0, valueBytes.Length);
            requsetStream.Close();
            return this;
        }

        public SingleHttpRequset SetFormData(string data)
        {
            //移除换行符
            data = data.Replace("\r", string.Empty);
            data = data.Replace("\n", string.Empty);
            //编码参数
            //arg = System.Web.HttpUtility.UrlEncode(arg);
            InnerHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

            var valueBytes = Encoding.GetBytes(data);
            InnerHttpWebRequest.ContentLength = valueBytes.Length;
            Stream requsetStream = InnerHttpWebRequest.GetRequestStream();
            requsetStream.Write(valueBytes, 0, valueBytes.Length);
            requsetStream.Close();
            return this;
        }

        #endregion 设置参数

        public string GetStringResult()
        {
            var result = string.Empty;
            var webResponse = InnerHttpWebRequest.GetResponse() as HttpWebResponse;
            if (webResponse != null)
            {
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        public JObject GetJObjectResult()
        {
            return JObject.Parse(GetStringResult());
        }
    }
}