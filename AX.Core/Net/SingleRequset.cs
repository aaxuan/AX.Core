using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace AX.Core.Net
{
    public static class SingleRequset
    {
        private static CookieContainer CookieContainer = new CookieContainer();

        public static HttpWebRequest HttpWebRequest;
        public static Encoding Encoding = Encoding.UTF8;

        public enum HttpMethod
        {
            GET,
            POST,
        }

        static SingleRequset()
        { } 

        public static void Init(string url, HttpMethod httpMethod)
        {
            Init(url, httpMethod.ToString());
        }

        public static void Init(string url, string httpMethod)
        {
            HttpWebRequest = null;
            HttpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            HttpWebRequest.Method = httpMethod;
            HttpWebRequest.CookieContainer = CookieContainer;
        }

        public static void SetJsonData<T>(T data)
        {
            HttpWebRequest.ContentType = "application/json;charset=UTF-8";
            var value = JsonConvert.SerializeObject(data);
            var valueBytes = Encoding.GetBytes(value);
            HttpWebRequest.ContentLength = valueBytes.Length;
            Stream requsetStream = HttpWebRequest.GetRequestStream();
            requsetStream.Write(valueBytes, 0, valueBytes.Length);
            requsetStream.Close();
        }

        public static void SetFormData(string data)
        {
            //移除换行符
            data = data.Replace("\r", string.Empty);
            data = data.Replace("\n", string.Empty);
            //编码参数
            //arg = System.Web.HttpUtility.UrlEncode(arg);
            HttpWebRequest.ContentType = "application/x-www-form-urlencoded";

            var valueBytes = Encoding.GetBytes(data);
            HttpWebRequest.ContentLength = valueBytes.Length;
            Stream requsetStream = HttpWebRequest.GetRequestStream();
            requsetStream.Write(valueBytes, 0, valueBytes.Length);
            requsetStream.Close();
        }

        public static String RunGetResult()
        {
            var result = string.Empty;
            var webResponse = HttpWebRequest.GetResponse() as HttpWebResponse;
            if (webResponse != null)
            {
                using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}