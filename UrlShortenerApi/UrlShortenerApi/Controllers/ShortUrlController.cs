using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.DTOs;
using UrlShortenerApi.Services;

namespace UrlShortenerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortUrlController : ControllerBase
    {
        private readonly IUrlService _urlService;
        private readonly IMapper _mapper;

        public ShortUrlController(IUrlService urlService, IMapper mapper)
        {
            _urlService = urlService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortUrl(ShortUrlCreateDto dto)
        {
            var shortUrl = await _urlService.CreateShortUrlAsync(dto);
            var result = _mapper.Map<ShortUrlReadDto>(shortUrl);
            return Ok(result);
        }

        [HttpGet("/r/{code}")]
        public async Task<IActionResult> RedirectToLongUrl(string code)
        {
            var shortUrl = await _urlService.GetByCodeAsync(code);
            if (shortUrl == null) return NotFound("Short URL not found or expired.");

            return Redirect(shortUrl.OriginalUrl);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetShortUrlInfo(string code)
        {
            var shortUrl = await _urlService.GetByCodeAsync(code);
            if (shortUrl == null) return NotFound();

            return Ok(_mapper.Map<ShortUrlReadDto>(shortUrl));
        }
    }
}
