using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.DTO
{
    public class FinalizeBooking
    {
        public int VillaId { get; set; }

        [ForeignKey("VillaId")]
        public VillaResponse? Villa { get; set; }

        public DateTime BookingDate { get; set; }
        public DateOnly CheckInDate { get; set; }
        [Required]
        public DateOnly CheckOutDate { get; set; }
        public int Nights { get; set; }

        public double TotalCost { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Status { get; set; }

        public Booking ToBooking()
        {
            return new Booking()
            {
                VillaId = VillaId,
                Villa = Villa?.ToVilla(),
                BookingDate = BookingDate,
                CheckInDate = CheckInDate,
                CheckOutDate = CheckOutDate,
                Nights = Nights,
                TotalCost = TotalCost,
                UserId = UserId,
                Name = Name!,   // model validation
                Email = Email!, // model validation
                Phone = Phone,
                Status = Status,
            };
        }
    }
}
