using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tools
{
    /// <summary>
    /// 可创建HttpClinet的FactoryTool,只适用于Net Core 2.1以上版本
    /// </summary>
    public class HttpTool
    {
        private static IHttpClientFactory _httpClientFactory;
        private static Lazy<HttpTool> instance = new Lazy<HttpTool>(() => { return new HttpTool(); }, true);
        /// <summary>
        /// HttpClinetFactory单例
        /// </summary>
        public static HttpTool Instance { get { return instance.Value; } }
        public static bool IsCreate { get { return instance.IsValueCreated; } }
        private HttpTool()
        {
            _httpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();
        }
        /// <summary>
        /// 简单异步Get请求
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string uri)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 简单异步Post请求
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="httpContent">请求Content</param>
        /// <returns></returns>
        public async Task<string> PostAsync(string uri, HttpContent httpContent)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(uri, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
        /// <summary>
        /// 获得一个HttpClient
        /// </summary>
        /// <param name="name">ClinetName</param>
        /// <returns></returns>
        public HttpClient GetHttpClient(string name="")
        {
            if(string.IsNullOrWhiteSpace(name))
                name= Microsoft.Extensions.Options.Options.DefaultName;
            return _httpClientFactory.CreateClient(name);
        }
    }

    public static class HttpClientExtensions
    {
        public static HttpClient SetKeepAlive(this HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
            return httpClient;
        }
    }
}
