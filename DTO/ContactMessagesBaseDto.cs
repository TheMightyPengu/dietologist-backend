namespace dietologist_backend.DTO
{
    public class ContactMessagesBaseDto
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
    }
}