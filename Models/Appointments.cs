namespace dietologist_backend.Models;

public class Appointments
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public ProvidedServices ProvidedService { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhone { get; set; }
    public bool IsPrepaid { get; set; }
}