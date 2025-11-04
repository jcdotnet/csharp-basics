namespace VillaBookingApp.Domain.Entities
{
    public class Villa
    {
        public  int Id { get; set; }

        //[MaxLength(50)] // if we want the name type to be nvarchar(50)
        // instead of varchar(max) in the database // highly recommended
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int SquareMeters { get; set; }
        public int Occupancy { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public IEnumerable<Amenity>? Amenities { get; set; } // navigation property

    }
}
