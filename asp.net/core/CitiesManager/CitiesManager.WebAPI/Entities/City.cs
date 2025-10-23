using System.ComponentModel.DataAnnotations;

namespace CitiesManager.WebAPI.Entities
{
    // Entity model for City
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
