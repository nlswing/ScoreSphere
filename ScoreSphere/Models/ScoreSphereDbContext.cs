namespace ScoreSphere.Models;
using Microsoft.EntityFrameworkCore;

public class ScoreSphereDbContext : DbContext
{
    internal DbSet<Match>? Matches { get; set; }
    internal DbSet<Team>? Teams { get; set; }

    internal string? DbPath { get; }

    internal string dbName = "scoresphere_dev";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(
          @"Host=localhost;Username=postgres;Password=1234;Database=" + this.dbName
        );
}