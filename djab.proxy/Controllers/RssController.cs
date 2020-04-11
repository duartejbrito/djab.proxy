using System.Threading.Tasks;
using djab.proxy.Models.Yts;
using djab.proxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace djab.proxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RssController : ControllerBase
    {
        private readonly ILogger<RssController> _logger;
        private readonly IYtsService _ytsService;
        public RssController(
            ILogger<RssController> logger,
            IYtsService ytsService)
        {
            _logger = logger;
            _ytsService = ytsService;
        }

        [HttpGet]
        [Route("{page:int=1}/{quality?}")]
        public Task<YtsResponse<YtsMovies>> Get(int page, string quality = "720p")
        {
            return _ytsService.Get(page, quality);
        }
    }
}