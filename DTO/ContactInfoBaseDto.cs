﻿namespace dietologist_backend.DTO
{
    public class ContactInfoBaseDto
    {
        public int Id { get; internal set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Location { get; set; }
    }
}