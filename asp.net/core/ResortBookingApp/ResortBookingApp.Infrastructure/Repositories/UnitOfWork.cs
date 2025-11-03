using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Infrastructure.Data;

namespace ResortBookingApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAmenityRepository Amenity { get; private set; }
        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Amenity = new AmenityRepository(db);
            Villa = new VillaRepository(db);
            VillaNumber = new VillaNumberRepository(db);
        }
    }
}
