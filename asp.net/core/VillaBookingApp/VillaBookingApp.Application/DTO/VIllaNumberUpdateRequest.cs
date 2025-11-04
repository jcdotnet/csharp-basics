using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.DTO
{
    public class VillaNumberUpdateRequest
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
    }
}
