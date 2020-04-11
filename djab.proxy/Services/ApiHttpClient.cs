using djab.proxy.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    public class ApiHttpClient<T> where T : IApiConfiguration
    {
        private HttpClient _client;

        public ApiHttpClient(T configuration)
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(configuration.BaseUrl),
            };
        }

        public async Task<TModel> Get<TModel>(string path)
        {
            var result = await _client.GetAsync(path);
            if (!result.IsSuccessStatusCode)
                HandleError(result);
            var response = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TModel>(response);
        }

        public static void HandleError(HttpResponseMessage result)
        {
            var message = result.Content.ReadAsStringAsync().Result;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("An error occurred while invoking API");
            stringBuilder.AppendLine($"[Status Code]: {(int)result.StatusCode}");
            stringBuilder.Append($"[Message]: {message}");

            throw new Exception(stringBuilder.ToString());
        }
    }
}
