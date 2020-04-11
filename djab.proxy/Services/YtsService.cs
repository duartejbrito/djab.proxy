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

        public async Task<YtsResponse<YtsMovies>> Get(int page, string quality = "720p")
        {
            YtsQuality parsedQuality = Enum<YtsQuality>.TryParseByDescription(quality);

            var response = await _client.Get<YtsResponse<YtsMovies>>($"/api/v2/list_movies.json?quality={parsedQuality.GetDescription()}&page={page}&sort_by=date_added");

            return response;
        }
    }
}
