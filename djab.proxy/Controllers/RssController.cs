using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AutoMapper;
using djab.proxy.Extensions;
using djab.proxy.Models.EzTv;
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
        private readonly IMapper _mapper;
        private readonly IYtsService _ytsService;
        private readonly IEzTvService _ezTvService;
        public RssController(
            ILogger<RssController> logger,
            IMapper mapper,
            IYtsService ytsService,
            IEzTvService ezTvService)
        {
            _logger = logger;
            _mapper = mapper;
            _ytsService = ytsService;
            _ezTvService = ezTvService;
        }

        [HttpGet]
        [Route("movies/{page:int=1}/{quality?}")]
        public FileContentResult Get(int page, string quality = "720p")
        {
            YtsQuality parsedQuality = Enum<YtsQuality>.TryParseByDescription(quality);
            var result = _ytsService.Get(page, parsedQuality).Result;
            return ToFeed(result);
        }

        [HttpGet]
        [Route("tvshows/{page:int=1}/{limit?}/{imdbId?}")]
        public FileContentResult Get(int page = 1, int limit = 20, long? imdbId = null)
        {
            var result = _ezTvService.Get(page, limit, imdbId).Result;
            return ToFeed(result);
        }

        private FileContentResult ToFeed(object result)
        {
            var feed = _mapper.Map<SyndicationFeed>(result);

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };
            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
            }
        }
    }
}