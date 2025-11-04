using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VillaBookingApp.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace VillaBookingApp.Application.DTO
{
    public class VillaNumberAddRequest
    {
        [Display(Name = "Villa Number")]
        public int Number { get; set; }
        public int VillaId { get; set; }

        [ValidateNever]
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
