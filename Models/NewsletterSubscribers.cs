namespace dietologist_backend.Models;

public class NewsletterSubscribers
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsSubscribed { get; set; }
    public DateTime SubscribedAt { get; set; }
}