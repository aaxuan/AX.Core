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
        public Encoding Encoding = Encoding.UTF8;

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

        public SingleHttpRequset Init(HttpMethod httpMethod, string url)
        {
            url.CheckIsNullOrWhiteSpace();
            if (url.StartsWith("http://", System.StringComparison.CurrentCultureIgnoreCase) == false && url.StartsWith("https://", System.StringComparison.CurrentCultureIgnoreCase) == false)
            { url = string.Concat("http://", url); }
            InnerHttpWebRequest = null;
            InnerHttpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            InnerHttpWebRequest.Method = httpMethod.ToString();
            InnerHttpWebRequest.CookieContainer = CookieContainer;
            return this;
        }

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

        //public static void SetFormData(string data)
        //{
        //    //移除换行符
        //    data = data.Replace("\r", string.Empty);
        //    data = data.Replace("\n", string.Empty);
        //    //编码参数
        //    //arg = System.Web.HttpUtility.UrlEncode(arg);
        //    HttpWebRequest.ContentType = "application/x-www-form-urlencoded";

        //    var valueBytes = Encoding.GetBytes(data);
        //    HttpWebRequest.ContentLength = valueBytes.Length;
        //    Stream requsetStream = HttpWebRequest.GetRequestStream();
        //    requsetStream.Write(valueBytes, 0, valueBytes.Length);
        //    requsetStream.Close();
        //}

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