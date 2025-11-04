namespace VillaBookingApp.Domain.Entities
{
    public class Amenity
    {
        // [Key] // the Id Property is the primary key by default
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int VillaId { get; set; }
        public Villa? Villa { get; set; } // navigation property
    }
}
