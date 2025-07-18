namespace UrlShortenerApi.DTOs
{
    public class ShortUrlCreateDto
    {
        public string OriginalUrl { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? CustomCode { get; set; }
    }
}
