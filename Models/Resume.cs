namespace dietologist_backend.Models;

public class Resume
{
    public int Id { get; set; }
    public string FileName { get; set; }        // Name of the CV file
    public string FileType { get; set; }        // MIME type (e.g., "application/pdf")
    public byte[] Data { get; set; }            // File data
    public DateTime UploadedAt { get; set; }
}
