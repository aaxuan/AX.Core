using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AX.Core.Network
{
    /// <summary>
    /// 适用于调用少数域名的链接帮助类
    /// </summary>
    public static class SingleHttpClient
    {
        private static readonly HttpClient client;

        static SingleHttpClient()
        {
            client = new HttpClient();
        }

        public static async Task<string> GetAsync(string url)
        {
            try
            {
                Uri uri = new(url);
                string responseBody = await client.GetStringAsync(uri);
                return responseBody;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<string> PostJsonAsync(string url, string jsonData)
        {
            try
            {
                Uri uri = new(url);
                HttpContent content = new StringContent(jsonData);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var responseMessage = await client.PostAsync(uri, content);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<string> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                var responseMessage = await client.SendAsync(httpRequestMessage);
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch
            {
                throw;
            }
        }
    }
}