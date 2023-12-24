using Microsoft.EntityFrameworkCore;
using Travel.Models;

namespace Travel.Contexts
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Destination> Destinations { get; set; }
    }
}
