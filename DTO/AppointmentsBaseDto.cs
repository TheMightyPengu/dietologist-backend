﻿namespace dietologist_backend.DTO
{
    public class AppointmentsBaseDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string ProvidedService { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public bool IsPrepaid { get; set; }
    }
}