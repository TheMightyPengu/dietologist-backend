namespace dietologist_backend.Models;

public class Articles
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Subtitle { get; set; }
    public string Heading { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime PublishedAt { get; set; }
}