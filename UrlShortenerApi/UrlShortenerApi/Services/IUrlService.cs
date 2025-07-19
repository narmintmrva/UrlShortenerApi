using UrlShortenerApi.DTOs;
using UrlShortenerApi.Models;

namespace UrlShortenerApi.Services
{
    public interface IUrlService
    {
        Task<ShortUrl> CreateShortUrlAsync(ShortUrlCreateDto dto);
        Task<ShortUrl> GetByCodeAsync(string code);
    }
}
