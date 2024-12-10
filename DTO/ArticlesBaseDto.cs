namespace dietologist_backend.DTO
{
    public class ArticlesBaseDto
    {
        public int Id { get; internal set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Heading { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}