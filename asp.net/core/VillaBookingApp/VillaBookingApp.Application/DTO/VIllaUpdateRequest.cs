using Microsoft.AspNetCore.Http;
using VillaBookingApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace VillaBookingApp.Application.DTO
{
    public class VillaUpdateRequest
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Range(1000, 1000000)]
        public double Price { get; set; }

        [Display(Name = "Square Meters")]
        public int SquareMeters { get; set; }

        [Range(1, 10)]
        public int Occupancy { get; set; }

        public IFormFile? Image { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }

        public Villa ToVilla()
        {
            return new Villa()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                SquareMeters = SquareMeters,
                Occupancy = Occupancy,
                ImageUrl = ImageUrl
            };
        }
    }
}
