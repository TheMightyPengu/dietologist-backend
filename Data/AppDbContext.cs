﻿using dietologist_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace dietologist_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Resume> Resume { get; set; }
    public DbSet<Recipes> Recipes { get; set; }
    public DbSet<Ebooks> Ebooks { get; set; }
    public DbSet<ProvidedServices> ProvidedServices { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<Articles> Articles { get; set; }
    public DbSet<SocialMediaLinks> SocialMediaLinks { get; set; }
    public DbSet<NewsletterSubscribers> NewsletterSubscribers { get; set; }
    public DbSet<Appointments> Appointments { get; set; }
    public DbSet<ContactInfo> ContactInfo { get; set; }
    public DbSet<ContactMessages> ContactMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}