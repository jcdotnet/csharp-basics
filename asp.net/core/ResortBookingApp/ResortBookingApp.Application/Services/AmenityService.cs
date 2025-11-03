using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Application.ServiceContracts;

namespace ResortBookingApp.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AmenityResponse> AddAmenity(AmenityAddRequest? amenity)
        {
            if (amenity is not null)
            {
                var fromDb = await _unitOfWork.Amenity.AddAsync(amenity.ToAmenity());
                return fromDb.ToAmenityResponse();
            }
            throw new ArgumentNullException(nameof(amenity));
        }

        public async Task<bool> AmenityExists(int? id)
        {
            return await _unitOfWork.Amenity.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> DeleteAmenity(int? id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));

            var fromDb = await _unitOfWork.Amenity.GetAsync(a => a.Id == id, "Villa");

            if (fromDb is null) { return false; }

            await _unitOfWork.Amenity.RemoveAsync(fromDb);
            return true;
        }

        public async Task<List<AmenityResponse>> GetAmenities()
        {
            var fromDb = await _unitOfWork.Amenity.GetAllAsync(includeProperties: "Villa");
            return fromDb.Select(a => a.ToAmenityResponse()).ToList();
        }

        public async Task<AmenityResponse?> GetAmenity(int? id)
        {
            var fromDb = await _unitOfWork.Amenity.GetAsync(v => v.Id == id, "Villa");

            if (fromDb is null) return null;

            return fromDb.ToAmenityResponse();
        }

        public async Task<AmenityResponse> UpdateAmenity(AmenityUpdateRequest? amenity)
        {
            if (amenity is null) throw new ArgumentNullException(nameof(amenity));

            var toDb = amenity.ToAmenity();
            await _unitOfWork.Amenity.UpdateAsync(toDb);

            return toDb.ToAmenityResponse();
        }
    }
}
