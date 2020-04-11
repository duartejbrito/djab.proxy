using djab.proxy.Injection;
using djab.proxy.Models.EzTv;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    [Injectable(Interface = typeof(IEzTvService))]
    public class EzTvService : IEzTvService
    {
        private ApiHttpClient<EzTvApiConfiguration> _client;

        public EzTvService(IOptions<EzTvApiConfiguration> configuration)
        {
            _client = new ApiHttpClient<EzTvApiConfiguration>(configuration.Value);
        }

        public async Task<EzTvResponse> Get(string imdbId, int page = 1, int limit = 20)
        {
            string path = $"/api/get-torrents?limit={limit}&page={page}";

            if (!string.IsNullOrEmpty(imdbId))
            {
                path = $"{path}&imdb_id={imdbId}";
            }

            var response = await _client.Get<EzTvResponse>(path);

            return response;
        }
    }
}
