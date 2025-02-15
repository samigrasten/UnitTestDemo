using Microsoft.EntityFrameworkCore;

namespace UnitTestDemo.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WeatherForecast>()
            .HasKey(w => w.Id);

        modelBuilder.Entity<WeatherForecast>()
            .Property(w => w.Id)
            .ValueGeneratedOnAdd();
    }
}