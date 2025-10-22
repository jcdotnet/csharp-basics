using ContactsManager.Enums;
using ContactsManager.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.DTO
{
    public class PersonUpdateRequest
    {
        [Required(ErrorMessage = "Person Id is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Person Name is required")]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public Guid? CountryId { get; set; } // FK
        public bool ReceiveNewsletters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Address = Address,
                BirthDate = BirthDate,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                ReceiveNewsletters = ReceiveNewsletters
            };
        }
    }
}
