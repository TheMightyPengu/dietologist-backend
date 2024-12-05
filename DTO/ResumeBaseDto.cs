namespace dietologist_backend.DTO
{
    public class ResumeBaseDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}