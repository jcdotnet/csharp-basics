
using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.DTO
{
    public class VillaNumberResponse
    {
        public int Number { get; set; }
        public int VillaId { get; set; }
        public Villa? Villa { get; set; } // navigation property
        public string? SpecialDetails { get; set; }

        public VillaNumber ToVillaNumber()
        {
            return new VillaNumber()
            {
                Number = Number,
                VillaId = VillaId,
                Villa = Villa,
                SpecialDetails = SpecialDetails
            };
        }
        public VillaNumberUpdateRequest ToVillaNumberUpdateRequest()
        {
            return new VillaNumberUpdateRequest()
            {
                Number = Number,
                VillaId = VillaId,
                Villa = Villa,
                SpecialDetails = SpecialDetails
            };
        }
    }

    public static class VillaNumberExtensions
    {
        public static VillaNumberResponse ToVillaNumberResponse(this VillaNumber villaNumber)
        {
            return new VillaNumberResponse()
            {
                Number = villaNumber.Number,
                VillaId = villaNumber.VillaId,
                Villa = villaNumber.Villa,
                SpecialDetails = villaNumber.SpecialDetails
            };
        }
    }
}
