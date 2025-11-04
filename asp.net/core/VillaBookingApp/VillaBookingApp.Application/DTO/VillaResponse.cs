using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VillaBookingApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace VillaBookingApp.Application.DTO
{
    public class VillaResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }

        [Display(Name = "Square Meters")]
        public int SquareMeters { get; set; }
        public int Occupancy { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        public IEnumerable<Amenity>? Amenities { get; set; } // navigation property

        public VillaUpdateRequest ToVillaUpdateRequest()
        {
            return new VillaUpdateRequest()
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

    public static class VillaExtensions
    {
        public static VillaResponse ToVillaResponse(this Villa villa)
        {
            return new VillaResponse()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Price = villa.Price,
                SquareMeters = villa.SquareMeters,
                Occupancy = villa.Occupancy,
                ImageUrl = villa.ImageUrl,
                Amenities = villa.Amenities,
            };
        }
    }
}
