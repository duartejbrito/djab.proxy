using AutoMapper;
using djab.proxy.Models.Yts;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace djab.proxy.Profiles
{
    public class YtsRssProfile : Profile
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public YtsRssProfile(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            CreateMap<YtsMovie, SyndicationItem>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new TextSyndicationContent(src.title)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.url))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTimeOffset.Parse(src.date_uploaded)))
                .BeforeMap((src, dest) => {
                    dest.AddPermalink(new Uri(src.url));
                    dest.ElementExtensions.Add(new XElement("enclosure",
                        new XAttribute("type", "application/x-bittorrent"),
                        new XAttribute("url", $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/movie/{src.torrents.First().url.Split("/").Last()}"),
                        new XAttribute("length", src.torrents.First().size_bytes)).CreateReader());
                });

            CreateMap<YtsResponse<YtsMovies>, SyndicationFeed>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new TextSyndicationContent("Movies rss feed")))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => new TextSyndicationContent("Movies rss feed")))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.data.movies));

        }
    }
}
