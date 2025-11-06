using VillaBookingApp.Application.DTO;

namespace VillaBookingApp.Application.ServiceContracts
{
    public interface IBookingService
    {
        Task<BookingResponse> AddBookingAsync(FinalizeBooking? booking);

        Task<IEnumerable<BookingResponse>> GetBookedVillas();
        Task<BookingResponse?> GetBookingAsync(int? bookingId);

        Task<IEnumerable<BookingResponse>> GetBookingsAsync();
        Task<IEnumerable<BookingResponse>> GetBookingsAsync(string? userId);
        Task<IEnumerable<BookingResponse>> GetBookingsAsync(string? userId, string? status);

        Task UpdateStatusAsync(int bookingId, string bookingStatus);

        Task UpdateStripePaymentAsync(
            int bookingId, 
            string sessionId, 
            string paymentIntentId
        );    
    }
}
