using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Domain.Entities;
using VillaBookingApp.Infrastructure.Data;

namespace VillaBookingApp.Infrastructure.Repositories
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        public readonly ApplicationDbContext _db;

        public AmenityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Amenity amenity)
        {
            _db.Amenities.Update(amenity);
            await _db.SaveChangesAsync();
        }
       
    }
}
