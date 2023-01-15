using AspNetTask.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetTask.Data;

public class ApplicationContext : DbContext
{
    public DbSet<ServiceObject> Services { get; set; } = null!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=AspNetTask.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceObject>().HasKey(i => i.Id);

        modelBuilder.Entity<ServiceObject>()
            .Property(n => n.Name).HasMaxLength(100);
    }

}