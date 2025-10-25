using System.ComponentModel.DataAnnotations;

namespace CitiesManager.Core.Entities
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
