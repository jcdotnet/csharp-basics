using VillaBookingApp.Application.DTO;
using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Application.Utility;


namespace VillaBookingApp.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookingResponse> AddBookingAsync(FinalizeBooking? booking)
        {
            if (booking is null) throw new ArgumentNullException(nameof(booking));
            
            var fromDb = await _unitOfWork.Booking.AddAsync(booking.ToBooking());
            return fromDb.ToBookingResponse();
        }

        public async Task<IEnumerable<BookingResponse>> GetActiveBookingsAsync()
        {
            var fromDb = await _unitOfWork.Booking.GetAllAsync(
                b => b.Status != SD.StatusPending || b.Status != SD.StatusCancelled, 
                includeProperties: "User,Villa"
            );
            return fromDb.Select(b => b.ToBookingResponse());
        }

        public async Task<IEnumerable<BookingResponse>> GetBookedVillas()
        {

            var fromDb = await _unitOfWork.Booking.GetAllAsync(
                b => b.Status == SD.StatusApproved || b.Status == SD.StatusCheckedIn,
                includeProperties: "User,Villa"
            );

            return fromDb.Select(b => b.ToBookingResponse());
        }

        public async Task<BookingResponse?> GetBookingAsync(int? bookingId)
        {
            var fromDb = await _unitOfWork.Booking.GetAsync(b => b.Id == bookingId,
                includeProperties: "User,Villa");

            if (fromDb is null) return null;

            return fromDb.ToBookingResponse();
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingsAsync()
        {
            var fromDb = await _unitOfWork.Booking.GetAllAsync(includeProperties: "User,Villa");

            return fromDb.Select(b => b.ToBookingResponse());
        }

        public async Task<IEnumerable<BookingResponse>> GetBookingsAsync(string? userId, string? status)
        {
            IEnumerable<Domain.Entities.Booking>? fromDb;
            if (string.IsNullOrEmpty(userId)) 
            {
                fromDb = await _unitOfWork.Booking.GetAllAsync(b => b.Status == status, "User,Villa");
            } else {
                fromDb = await _unitOfWork.Booking.GetAllAsync(b => b.UserId == userId & b.Status == status,
                   includeProperties: "User,Villa");
            }

            return fromDb.Select(b => b.ToBookingResponse());
        }

        public async Task UpdateStatusAsync(int bookingId, string bookingStatus)
        {
            var fromDb = await _unitOfWork.Booking.GetAsync(b => b.Id == bookingId, "User,Villa");
            if (fromDb == null) throw new ArgumentException("Invalid Booking Id");

            fromDb.Status = bookingStatus;
            if (bookingStatus == SD.StatusCheckedIn)
                fromDb.ActualCheckInDate = DateTime.Now;
            if (bookingStatus == SD.StatusCompleted)
                fromDb.ActualCheckOutDate = DateTime.Now;
            await _unitOfWork.Booking.UpdateAsync(fromDb);    
        }

        public async Task UpdateStripePaymentAsync(
            int bookingId, 
            string sessionId, 
            string paymentIntentId)
        {
            var fromDb = await _unitOfWork.Booking.GetAsync(b => b.Id == bookingId);
            if (fromDb == null) throw new ArgumentException("Invalid booking Id");
            if (!string.IsNullOrEmpty(sessionId))
            {
                fromDb.StripeSessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                fromDb.StripePaymentIntentId = paymentIntentId;
                fromDb.PaymentDate = DateTime.Now;
                fromDb.IsPaymentSuccessful = true;
            }
            // updated service lifetime to avoid InvalidOperation exception here below
            // The instance of entity type 'Booking' cannot be tracked because another
            // instance with the same key value for {'Id'} is already being tracked...
            //await _unitOfWork.Booking.UpdateAsync(booking); // it's not that, later
            await _unitOfWork.SaveAsync();
        }
    }
}