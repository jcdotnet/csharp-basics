using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.DTO
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public int VillaId { get; set; }
        public VillaResponse? Villa { get; set; }

        public DateTime BookingDate { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public int Nights { get; set; }

        public double TotalCost { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Status { get; set; }

        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }
    }

    public static class BookingExtensions
    {
        public static BookingResponse ToBookingResponse(this Booking booking)
        {
            return new BookingResponse()
            {
                Id = booking.Id,
                VillaId = booking.VillaId,
                Villa = booking.Villa?.ToVillaResponse(),
                BookingDate = booking.BookingDate,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                Nights = booking.Nights,
                TotalCost = booking.TotalCost,
                UserId = booking.UserId,
                User = booking.User,
                Name = booking.Name,
                Email = booking.Email,
                Phone = booking.Phone,
                Status = booking.Status,
                StripeSessionId = booking.StripeSessionId,
                StripePaymentIntentId = booking.StripePaymentIntentId,
                IsPaymentSuccessful = booking.IsPaymentSuccessful,
                PaymentDate = booking.PaymentDate,
                ActualCheckInDate = booking.ActualCheckInDate,
                ActualCheckOutDate = booking.ActualCheckOutDate,
            };
        }
    }
}
