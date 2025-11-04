using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.DTO
{
    public class AmenityResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public string? Description { get; set; }

        public int VillaId { get; set; }

        [ValidateNever]
        public Villa? Villa { get; set; } // navigation property

        public AmenityUpdateRequest ToAmenityUpdateRequest()
        {
            return new AmenityUpdateRequest()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                VillaId = VillaId,
                Villa = Villa
            };
        }
    }

    public static class AmenityExtensions
    {
        public static AmenityResponse ToAmenityResponse(this Amenity amenity)
        {
            return new AmenityResponse()
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Description = amenity.Description,
                VillaId = amenity.VillaId,
                Villa = amenity.Villa
            };
        }
    }
}
