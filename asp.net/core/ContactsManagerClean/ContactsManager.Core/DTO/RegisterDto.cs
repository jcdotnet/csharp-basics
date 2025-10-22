using ContactsManager.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class RegisterDto
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "ValidateEmail", controller: "Account", ErrorMessage = "Email is taken")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]{3,14}$", ErrorMessage="Phone number should contain 3-15 digits")]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

        public UserRole UserRole { get; set; } = UserRole.User;
    }
}
