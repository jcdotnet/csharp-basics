using ResortBookingApp.Application.DTO;

namespace ResortBookingApp.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<VillaResponse>? VillasList { get; set; }

        public DateOnly CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public int Nights { get; set; }

    }
}
