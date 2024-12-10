namespace dietologist_backend.DTO
{
    public class SocialMediaLinksBaseDto
    {
        public int Id { get; internal set; }
        public string PlatformName { get; set; }
        public string Url { get; set; }
    }
}