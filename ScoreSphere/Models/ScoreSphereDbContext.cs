namespace ScoreSphere.Models;
using Microsoft.EntityFrameworkCore;

public class ScoreSphereDbContext : DbContext
{
    internal DbSet<Match>? Matches { get; set; }
    internal DbSet<Team>? Teams { get; set; }
    internal DbSet<User> Users {get; set;}

    internal string? DbPath { get; }

    internal string dbName = "scoresphere_dev";

    public ScoreSphereDbContext(DbContextOptions<ScoreSphereDbContext> options) : base(options)
{
}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Your model configuration here
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
          @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
}

