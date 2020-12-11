using System.Text;
using Microsoft.EntityFrameworkCore;
using NGK_aflevering_3_1;

namespace NGK_aflevering_3_1
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlite("Data Source=WeatherForecast.db");
        }
        //Seb: @"Data Source=localhost,1433;Database=vareDatabase;User ID=SA;Password=SecPass1;"
        //Erm: @"Data Source=(localdb)\MSSQLLocalDB;TrustServerCertificate=False;MultiSubnetFailover=False;database=testDB;"
        //Gus: @"Data Source=localhost,1433;Database=vareDatabase;User ID=SA;Password=Password1!;"

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Location> Locations { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WeatherForecast>()
                .HasOne(a => a.Location)
                .WithOne(b => b.WeatherForecast)
                .HasForeignKey<Location>(e => e.WeatherForecastId);

            //modelBuilder.ApplyConfiguration(new WeatherForecastConfigurations());
            //modelBuilder.ApplyConfiguration(new LocationConfigurations());
        }
        public DbSet<User> Users { get; set; }

        public DbSet<NGK_aflevering_3_1.User> User { get; set; }
    }
}
