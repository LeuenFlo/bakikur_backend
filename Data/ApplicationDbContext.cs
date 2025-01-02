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
    public DbSet<ProjectImage> ProjectImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectImage>()
            .HasOne(pi => pi.Project)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 