using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.DTO
{
    public class AmenityAddRequest
    {

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int VillaId { get; set; }

        [ValidateNever]
        public Villa? Villa { get; set; } // navigation property

        public Amenity ToAmenity()
        {
            return new Amenity()
            {
                Name = Name,
                Description = Description,
                VillaId = VillaId,
                Villa = Villa
            };
        }
    }
}
