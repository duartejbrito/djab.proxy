using djab.proxy.Extensions;
using djab.proxy.Injection;
using djab.proxy.Models.Yts;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace djab.proxy.Services
{
    [Injectable(Interface = typeof(IYtsService))]
    public class YtsService : IYtsService
    {
        private ApiHttpClient<YtsApiConfiguration> _client;

        public YtsService(IOptions<YtsApiConfiguration> configuration)
        {
            _client = new ApiHttpClient<YtsApiConfiguration>(configuration.Value);
        }

        public async Task<YtsResponse<YtsMovies>> Get(int page, YtsQuality quality = YtsQuality.Low)
        {
            var response = await _client.Get<YtsResponse<YtsMovies>>($"/api/v2/list_movies.json?quality={quality.GetDescription()}&page={page}&sort_by=date_added");

            return response;
        }
    }
}
