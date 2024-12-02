namespace dietologist_backend.Models;

public class ProvidedServices
{
    public int Id { get; set; }
    public string Category { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
    public decimal PriceIncludingVAT { get; set; }
    public TimeSpan Interval { get; set; }
}