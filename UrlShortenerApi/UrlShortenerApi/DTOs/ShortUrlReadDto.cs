namespace UrlShortenerApi.DTOs
{
    public class ShortUrlReadDto
    {
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ClickCount { get; set; }
    }
}
