using AutoMapper;
using UrlShortenerApi.DTOs;
using UrlShortenerApi.Models;

namespace UrlShortenerApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShortUrl, ShortUrlReadDto>();
            CreateMap<ShortUrlCreateDto, ShortUrl>();
        }
    }
}
