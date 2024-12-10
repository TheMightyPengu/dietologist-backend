namespace dietologist_backend.DTO
{
    public class EbooksBaseDto
    {
        public int Id { get; internal set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string TableOfContents { get; set; }
        public string CoverImageUrl { get; set; }
        public decimal Price { get; set; }
        public string FileUrl { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}