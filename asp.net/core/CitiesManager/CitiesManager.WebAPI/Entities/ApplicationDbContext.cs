using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
           
        }

        public virtual DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(new City()
            {
                Id = Guid.Parse("EB65F42D-C9C4-4997-86D9-FC31795081EE"),
                Name = "Madrid"
            });
            modelBuilder.Entity<City>().HasData(new City() { 
                Id = Guid.Parse("0DBF624E-7440-463F-A68E-0BC058DDF407"),
                Name = "Málaga" 
            });
            modelBuilder.Entity<City>().HasData(new City()
            {
                Id = Guid.Parse("21442586-D075-4948-ADBF-1BD0C443A046"),
                Name = "London"
            });
        }
    }
}
