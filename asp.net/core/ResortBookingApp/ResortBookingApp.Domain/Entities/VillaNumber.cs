using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortBookingApp.Domain.Entities
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Number { get; set; }

        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa? Villa { get; set; } // navigation property

        public string? SpecialDetails { get; set; }
    }
}
