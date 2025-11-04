using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ResortBookingApp.Web.Models
{
    public class LoginViewModel
    {
        // user identity props with Remember Password, RedirectUrl, etc
        // maybe to create a DTO login object and use it here in the vm
        // option 3) to pass the DTO only to the view with the viewdata
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool Remember { get; set; }
        public string? RedirectUrl { get; set; }
    }
}
