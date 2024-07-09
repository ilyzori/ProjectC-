// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using CustomApp.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
