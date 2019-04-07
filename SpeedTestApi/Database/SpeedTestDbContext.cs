using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeedTestApi.Database.Models;

namespace SpeedTestApi.Database
{
    public class SpeedTestDbContext : DbContext
    {
        public SpeedTestDbContext(DbContextOptions<SpeedTestDbContext> options) : base(options) { }

        public DbSet<SpeedTestResult> SpeedTestResults { get; set; }
    }
}
