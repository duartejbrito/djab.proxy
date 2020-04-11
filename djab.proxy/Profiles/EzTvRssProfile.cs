using AutoMapper;
using djab.proxy.Models.EzTv;
using Microsoft.AspNetCore.Http;
using System;
using System.ServiceModel.Syndication;
using System.Xml.Linq;

namespace djab.proxy.Profiles
{
    public class EzTvRssProfile : Profile
    {
        public EzTvRssProfile()
        {
            CreateMap<EzTvTorrent, SyndicationItem>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new TextSyndicationContent(src.title)))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.episode_url))
                .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.date_released_unix)))
                .BeforeMap((src, dest) => dest.ElementExtensions.Add(new XElement("enclosure",
                        new XAttribute("type", "application/x-bittorrent"),
                        new XAttribute("url", src.torrent_url),
                        new XAttribute("length", src.size_bytes)
                    ).CreateReader()));

            CreateMap<EzTvResponse, SyndicationFeed>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => new TextSyndicationContent("TvShows rss feed")))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => new TextSyndicationContent("TvShows rss feed")))
                .ForMember(dest => dest.LastUpdatedTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.torrents));
        }
    }
}
