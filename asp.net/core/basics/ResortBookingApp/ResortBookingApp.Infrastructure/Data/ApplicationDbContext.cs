using Microsoft.EntityFrameworkCore;
using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Villa> Villas { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 200,
                    SquareMeters = 50,
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 4,
                    Price = 300,
                    SquareMeters = 50,
                },
                new Villa
                {
                    Id = 3,
                    Name = "Luxury Pool Villa",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 4,
                    Price = 400,
                    SquareMeters = 70,
                }
            );
        }
    }
}
