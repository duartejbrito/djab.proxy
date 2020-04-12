using CloudflareSolverRe;
using CloudflareSolverRe.Types;
using djab.proxy.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    public class ApiHttpClient<T> where T : IApiConfiguration
    {

        private CloudflareSolver _cf;
        private HttpClientHandler _handler;
        private HttpClient _client;

        public ApiHttpClient(T configuration)
        {
            if (configuration.ByPass)
            {
                _cf = new CloudflareSolver
                {
                    MaxTries = 3,
                    ClearanceDelay = 3000
                };
            }

            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler)
            {
                BaseAddress = new Uri(configuration.BaseUrl),
            };
        }

        public async Task<TModel> Get<TModel>(string path)
        {
            if (_cf != null)
            {
                var resultCloudflare = await _cf.Solve(_client, _handler, new Uri(_client.BaseAddress, path));
                if (!resultCloudflare.Success)
                    HandleError(resultCloudflare);
            }

            var result = await _client.GetAsync(path);
            if (!result.IsSuccessStatusCode)
                HandleError(result);
            var response = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TModel>(response);
        }

        public async Task<FileStreamResult> File(string path)
        {
            if (_cf != null)
            {
                var resultCloudflare = await _cf.Solve(_client, _handler, new Uri(_client.BaseAddress, path));
                if (!resultCloudflare.Success)
                    HandleError(resultCloudflare);
            }

            var result = await _client.GetAsync(path);
            if (!result.IsSuccessStatusCode)
                HandleError(result);
            var response = await result.Content.ReadAsStreamAsync();
            return new FileStreamResult(response, result.Content.Headers.ContentType.MediaType)
            {
                FileDownloadName = result.Content.Headers.ContentDisposition.FileName.Trim('"')
            };
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

        public static void HandleError(SolveResult result)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("An error occurred while bypassing cloudflare");
            stringBuilder.AppendLine($"[Reason]: {result.FailReason}");

            throw new Exception(stringBuilder.ToString());
        }
    }
}
