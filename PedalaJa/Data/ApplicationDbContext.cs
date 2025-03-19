using Microsoft.EntityFrameworkCore;
using PedalaJa.Models;
using static System.Collections.Specialized.BitVector32;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Network> Networks { get; set; }
    public DbSet<Station> Stations { get; set; }
    public DbSet<StationRating> StationRatings { get; set; }
}
