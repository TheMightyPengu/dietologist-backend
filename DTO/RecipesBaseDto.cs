﻿namespace dietologist_backend.DTO
{
    public class RecipesBaseDto
    {
        public int Id { get; internal set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}