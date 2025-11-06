using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Security.Claims;
using VillaBookingApp.Application.DTO;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Application.Utility;
using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IVillaService _villaService;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingsController(IBookingService bookingService, IVillaService villaService,
            UserManager<ApplicationUser> userManager)
        {
            _bookingService = bookingService;
            _villaService = villaService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            string userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value!; // not null

            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound(nameof(user)); // should not be null

            var villa = await _villaService.GetVilla(villaId);
            if (villa == null) return NotFound(nameof(villa)); // should not be null

            FinalizeBooking booking = new()
            {
                VillaId = villaId,
                Villa = villa,
                Nights = nights,
                CheckInDate = checkInDate,
                CheckOutDate = checkInDate.AddDays(nights),
                TotalCost = villa.Price * nights,
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                Phone = user.PhoneNumber
            };
            return View(booking);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FinalizeBooking(FinalizeBooking booking)
        {
            var villa = await _villaService.GetVilla(booking.VillaId);
            if (villa == null) return NotFound(villa); //
            booking.TotalCost = villa.Price * booking.Nights;
            booking.Status = SD.StatusPending;
            booking.BookingDate = DateTime.Now;

            // to check availability before adding the booking to the database:
            // int roomAvailable = SD.RoomsAvailable
            // if (roomAvailable == 0)
            // set error to TempData and redirect to FinalizeBooking

            var bookingResponse = await _bookingService.AddBookingAsync(booking);

            // redirecting to the booking confirmation page after payment
            //return RedirectToAction(nameof(BookingConfirmation), new { BookingId = bookingResponse.Id });

            // stripe
            var domain = Request.Scheme + "://" + Request.Host.Value + "/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"bookings/BookingConfirmation?bookingId={bookingResponse.Id}",
                CancelUrl = domain + $"bookings/FinalizeBooking?villaId={booking.VillaId}&checkInDate={booking.CheckInDate}&nights={booking.Nights}",
            };

            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(booking.TotalCost * 100),
                    Currency = "EUR",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = villa.Name
                        //Images = new List<string> { domain + villa.ImageUrl },
                    },
                },
                Quantity = 1,
            });

            var service = new SessionService();
            Session session = service.Create(options);

            await _bookingService.UpdateStripePaymentAsync(bookingResponse.Id, session.Id, session.PaymentIntentId);

            Response.Headers?.Append("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [Authorize]
        public async Task<IActionResult> BookingConfirmation(int bookingId)
        {
            var fromDb = await _bookingService.GetBookingAsync(bookingId);
            if(fromDb?.Status == SD.StatusPending)
            {
                //var service = new SessionService();
                //Session session = service.Get(fromDb.StripeSessionId); // session id not stored in the DB (to go over this)
                //if (session.PaymentStatus == "paid")
                //{
                //    await _bookingService.UpdateStatusAsync(fromDb.Id, SD.StatusApproved);
                //    await _bookingService.UpdateStripePaymentAsync(fromDb.Id, session.Id, session.PaymentIntentId);
                //}
            }
            return View(bookingId);
        }

        [Authorize]
        public async Task<IActionResult> BookingDetails(int bookingId)
        {
            var fromDb = await _bookingService.GetBookingAsync(bookingId);
            return View(fromDb);
        }

        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> CheckIn(Booking booking)
        {
            await _bookingService.UpdateStatusAsync(booking.Id, SD.StatusCheckedIn);
            TempData["Success"] = "Booking Updated Successfully.";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> CheckOut(Booking booking)
        {
            await _bookingService.UpdateStatusAsync(booking.Id, SD.StatusCompleted);
            TempData["Success"] = "Booking Completed Successfully.";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> CancelBooking(Booking booking)
        {
            await _bookingService.UpdateStatusAsync(booking.Id, SD.StatusCancelled);
            TempData["Success"] = "Booking Cancelled Successfully.";
            return RedirectToAction(nameof(BookingDetails), new { bookingId = booking.Id });
        }

        // Datatable API call // https://datatables.net/
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string? status)
        {
            IEnumerable<BookingResponse> bookings;
            string? userId = "";

            if (!User.IsInRole(SD.RoleAdmin))
            {
                var claimsIdentity = (ClaimsIdentity?)User.Identity;
                userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            bookings = await _bookingService.GetBookingsAsync(userId, status);

            return Json(new { data = bookings });
        }
    }
}
