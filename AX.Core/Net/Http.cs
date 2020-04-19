using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace AX.Framework.Net
{
    public static class Http
    {
        /// <summary>
        /// 创建 HttpClient
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

        public static string Get(string url, Dictionary<string, string> arg = null)
        {
            var client = CreateHttpClient(url);

            if (arg != null && arg.Count >= 0)
            {
                var questionMarkIndex = url.IndexOf("?");
                if (questionMarkIndex <= 0)
                {
                    url += "?";
                }
                foreach (var item in arg)
                {
                    url += string.Format("{0}={1}&", item.Key, item.Value);
                }
                url.TrimEnd('&');
            }

            return client.GetStringAsync(url).Result;
        }

        public static MemoryStream DownloadData(string url)
        {
            using (WebClient client = new WebClient())
            {
                MemoryStream ms = new MemoryStream(client.DownloadData(url));
                return ms;
            }
        }
    }
}