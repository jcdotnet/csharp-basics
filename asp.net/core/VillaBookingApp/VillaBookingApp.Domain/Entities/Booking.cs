using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaBookingApp.Domain.Entities
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public required string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }

        public int VillaId { get; set; }
        
        [ForeignKey("VillaId")]
        public Villa? Villa { get; set; }

        [StringLength(40)] // nvarchar(40)
        public required string Name { get; set; }

        [StringLength(40)] // nvarchar(40)
        public required string Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public double TotalCost { get; set; }
        public int Nights { get; set; }
        public string? Status { get; set; }

        public DateTime BookingDate { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }

        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }

        public int VillaNumber { get; set; }    
    }
}
