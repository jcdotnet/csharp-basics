using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Domain.Entities;
using ResortBookingApp.Infrastructure.Data;

namespace ResortBookingApp.Infrastructure.Repositories
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
