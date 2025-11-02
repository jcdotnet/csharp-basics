using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Domain.Entities;
using ResortBookingApp.Infrastructure.Data;

namespace ResortBookingApp.Infrastructure.Repositories
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task UpdateAsync(VillaNumber villaNumber)
        {
            _db.VillaNumbers.Update(villaNumber);
            await _db.SaveChangesAsync();
        }
    }
}
