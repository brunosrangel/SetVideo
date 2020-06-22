using setVideo.Model;
using setVideo.Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace setVideo.Repository
{

    public class setVideoContext : DbContext
    {
  
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Location> Locations { get; set; }

        public setVideoContext(DbContextOptions<setVideoContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
        }
    }

}
