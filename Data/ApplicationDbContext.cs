using Microsoft.EntityFrameworkCore;
using BakikurBackend.Models;

namespace BakikurBackend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
} 