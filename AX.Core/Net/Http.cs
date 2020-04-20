using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AX.Framework.Net
{
    public static class Http
    {
        static Http()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        }

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

        public static string PostJsonData(string url, string datavalue)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            var dataByte = Encoding.UTF8.GetBytes(datavalue);
            req.ContentLength = dataByte.Length;

            using (var reqStream = req.GetRequestStream())
            {
                reqStream.Write(dataByte, 0, dataByte.Length);
                reqStream.Close();
            }

            var resp = (HttpWebResponse)req.GetResponse();
            var stream = resp.GetResponseStream();
            //获取响应内容
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var result = reader.ReadToEnd();
                return result;
            }
        }

        //public static MemoryStream DownloadData(string url)
        //{
        //    using (WebClient client = new WebClient())
        //    {
        //        MemoryStream ms = new MemoryStream(client.DownloadData(url));
        //        return ms;
        //    }
        //}

        //public static string Post(string url, Dictionary<string, string> dic)
        //{
        //    HttpWebRequest request = null;
        //    request = WebRequest.Create(url) as HttpWebRequest;
        //    request.ProtocolVersion = HttpVersion.Version10;
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        //    //POST参数拼接
        //    if (!(dic == null || dic.Count == 0))
        //    {
        //        StringBuilder buffer = new StringBuilder();
        //        int i = 0;
        //        foreach (string key in dic.Keys)
        //        {
        //            if (i > 0)
        //            {
        //                buffer.AppendFormat("&{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key]));
        //            }
        //            else
        //            {
        //                buffer.AppendFormat("{0}={1}", key, System.Web.HttpUtility.UrlEncode(dic[key]));
        //            }
        //            i++;
        //        }
        //        Console.WriteLine(url.ToString());
        //        Console.WriteLine(buffer.ToString());
        //        byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
        //        using (Stream stream = request.GetRequestStream())
        //        {
        //            stream.Write(data, 0, data.Length);
        //        }
        //    }
        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //        using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
        //        {
        //            return reader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}