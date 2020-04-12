using System.Threading.Tasks;
using AutoMapper;
using djab.proxy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace djab.proxy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMapper _mapper;
        private readonly IYtsService _ytsService;

        public MovieController(
            ILogger<MovieController> logger,
            IMapper mapper,
            IYtsService ytsService)
        {
            _logger = logger;
            _mapper = mapper;
            _ytsService = ytsService;
        }

        [HttpGet]
        [Route("{torrent}")]
        public async Task<FileStreamResult> Get(string torrent)
        {
            return await _ytsService.Torrent(torrent);
        }
    }
}