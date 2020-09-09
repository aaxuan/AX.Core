using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AX.Core.Net
{
    public static class Http
    {
        static Http()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.DefaultConnectionLimit = 1024;
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// 创建 HttpClient
        /// </summary>
        /// <param name="url"> </param>
        /// <returns> </returns>
        public static HttpClient CreateHttpClient(string url)
        {
            HttpClient httpclient;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                httpclient = new HttpClient();
            }
            else
            {
                httpclient = new HttpClient();
            }
            return httpclient;
        }

        public static string Get(string url)
        {
            var cli = CreateHttpClient(url);
            var result = cli.GetStringAsync(url).Result;
            return result;
        }

        public static string Get(string url, Dictionary<string, string> dic = null)
        {
            var cli = CreateHttpClient(url);
            StringBuilder parm = new StringBuilder();
            int i = 0;
            foreach (string key in dic.Keys)
            {
                if (i > 0)
                { parm.AppendFormat("&{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key])); }
                else
                { parm.AppendFormat("?{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key])); }
                i++;
            }
            var result = cli.GetStringAsync(url + parm.ToString()).Result;
            return result;
        }

        public static string PostJson(string url, string content, Encoding encoding = null)
        {
            if (encoding == null)
            { encoding = Encoding.UTF8; }

            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";

            var data = encoding.GetBytes(content);
            //写入请求流
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse() as HttpWebResponse;
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public static string PostForm(string url, Dictionary<string, string> dic, Encoding encoding = null)
        {
            if (encoding == null)
            { encoding = Encoding.UTF8; }

            HttpWebRequest request = null;
            request = WebRequest.Create(url) as HttpWebRequest;
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            //POST参数拼接
            if (!(dic == null || dic.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in dic.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key]));
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key]));
                    }
                    i++;
                }
                Console.WriteLine(url.ToString());
                Console.WriteLine(buffer.ToString());
                byte[] data = encoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}