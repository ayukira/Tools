using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tools.HttpTool
{
    public class HttpTool
    {
        private static IHttpClientFactory _httpClientFactory;
        private static Lazy<HttpTool> instance = new Lazy<HttpTool>(() => { return new HttpTool(); }, true);

        public static HttpTool Instance { get { return instance.Value; } }
        public static bool IsCreate { get { return instance.IsValueCreated; } }
        private HttpTool()
        {
            _httpClientFactory = new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();
        }

        public static async Task<string> GetAsync(string uri)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> PostAsync(string uri, HttpContent httpContent)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(uri, httpContent);
            return await response.Content.ReadAsStringAsync();
        }

        public static HttpClient GetHttpClient(string name="")
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
