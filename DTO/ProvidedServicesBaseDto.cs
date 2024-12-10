namespace dietologist_backend.DTO;

public class ProvidedServicesBaseDto
{
    public int Id { get; internal set; }
    public string Category { get; set; }
    public int Duration { get; set; }
    public string Description { get; set; }
    public decimal PriceIncludingVAT { get; set; }
    public int IntervalInDays { get; set; }
}