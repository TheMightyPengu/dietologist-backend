namespace dietologist_backend.Models;

public class Resume
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public byte[] Data { get; set; }
    public DateTime UploadedAt { get; set; }
}