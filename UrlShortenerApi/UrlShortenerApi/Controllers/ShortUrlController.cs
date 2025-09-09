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
        [ProducesResponseType(typeof(ShortUrlReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortUrl(ShortUrlCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.OriginalUrl))
                return BadRequest("Original URL is required.");

            var shortUrl = await _urlService.CreateShortUrlAsync(dto);
            var result = _mapper.Map<ShortUrlReadDto>(shortUrl);
            return Ok(result);
        }

        [HttpGet("/r/{code}")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RedirectToLongUrl(string code)
        {
            var shortUrl = await _urlService.GetByCodeAsync(code);
            if (shortUrl == null) return NotFound("Short URL not found or expired.");

            return Redirect(shortUrl.OriginalUrl);
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(ShortUrlReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShortUrlInfo(string code)
        {
            var shortUrl = await _urlService.GetByCodeAsync(code);
            if (shortUrl == null) return NotFound();

            return Ok(_mapper.Map<ShortUrlReadDto>(shortUrl));
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteShortUrl(string code)
        {
            var deleted = await _urlService.DeleteByCodeAsync(code);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShortUrlReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShortUrls()
        {
            var urls = await _urlService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ShortUrlReadDto>>(urls));
        }

    }
}
