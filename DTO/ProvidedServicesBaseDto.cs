namespace dietologist_backend.DTO.ProvidedServicesDTOs;

public class ProvidedServicesBaseDto
{
    public string Category { get; set; }
    public TimeSpan Duration { get; set; }
    public string Description { get; set; }
    public decimal PriceIncludingVAT { get; set; }
    public TimeSpan Interval { get; set; }
}