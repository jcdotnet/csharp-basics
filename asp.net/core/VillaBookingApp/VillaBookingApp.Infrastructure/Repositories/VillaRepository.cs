using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Domain.Entities;
using VillaBookingApp.Infrastructure.Data;

namespace VillaBookingApp.Infrastructure.Repositories
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(Villa villa) 
        {
            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
        }
    }
}
