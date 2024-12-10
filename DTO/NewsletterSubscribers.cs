namespace dietologist_backend.DTO
{
    public class NewsletterSubscribersBaseDto
    {
        public int Id { get; internal set; }
        public string Email { get; set; }
        public bool IsSubscribed { get; set; }
        public DateTime SubscribedAt { get; set; }
    }
}