namespace dietologist_backend.Models;

public class Images
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime UploadedAt { get; set; }
}