namespace dietologist_backend.Models;

public class ContactMessages
{
    public int Id { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
    public string Message { get; set; }
    public DateTime SentAt { get; set; }
}