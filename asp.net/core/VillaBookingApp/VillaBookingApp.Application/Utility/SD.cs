using VillaBookingApp.Application.DTO;

namespace VillaBookingApp.Application.Utility
{
    public static class SD // static details
    {
        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusCheckedIn = "CheckedIn";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public static int RoomsAvailable(int villaId,
            List<VillaNumberResponse>? villaNumbers, DateOnly checkInDate, int nights,
           List<BookingResponse>? bookings)
        {
            if (villaNumbers == null) return 0; 

            List<int> bookingInDate = new();
            int finalAvailableRoomForAllNights = int.MaxValue;
            var roomsInVilla = villaNumbers.Where(x => x.VillaId == villaId).Count();

            for (int i = 0; i < nights; i++)
            {
                if (bookings != null)
                {
                    var villasBooked = bookings.Where(b => b.CheckInDate <= checkInDate.AddDays(i)
                        && b.CheckOutDate > checkInDate.AddDays(i) && b.VillaId == villaId);

                    foreach (var booking in villasBooked)
                    {
                        if (!bookingInDate.Contains(booking.Id))
                        {
                            bookingInDate.Add(booking.Id);
                        }
                    }
                }
                var totalAvailableRooms = roomsInVilla - bookingInDate.Count;
                if (totalAvailableRooms == 0)
                {
                    return 0;
                }
                else
                {
                    if (finalAvailableRoomForAllNights > totalAvailableRooms)
                    {
                        finalAvailableRoomForAllNights = totalAvailableRooms;
                    }
                }
            }
            return finalAvailableRoomForAllNights;
        }
    }
}
