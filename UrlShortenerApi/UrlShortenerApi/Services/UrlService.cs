using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using UrlShortenerApi.DTOs;
using UrlShortenerApi.Models;

namespace UrlShortenerApi.Services
{
    public class UrlService : IUrlService
    {
        private readonly AppDbContext _context;

        public UrlService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShortUrl> CreateShortUrlAsync(ShortUrlCreateDto dto)
        {
            var code = string.IsNullOrWhiteSpace(dto.CustomCode)
                ? Guid.NewGuid().ToString("N")[..6]
                : dto.CustomCode;

            if (await _context.ShortUrls.AnyAsync(x => x.ShortCode == code))
                throw new Exception("Short code already exists.");

            var shortUrl = new ShortUrl
            {
                OriginalUrl = dto.OriginalUrl,
                ShortCode = code,
                ExpiryDate = dto.ExpiryDate,
            };

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            return shortUrl;
        }

        public async Task<ShortUrl> GetByCodeAsync(string code)
        {
            var result = await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == code);

            if (result == null || (result.ExpiryDate != null && result.ExpiryDate < DateTime.UtcNow))
                return null;

            result.ClickCount++;
            await _context.SaveChangesAsync();

            return result;
        }
    }
}
