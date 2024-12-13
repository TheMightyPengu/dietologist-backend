namespace dietologist_backend.DTO
{
    public class ResumeBaseDto
    {
        public int Id { get; internal set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
       // public byte[] Data { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}