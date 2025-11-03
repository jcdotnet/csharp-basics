using ResortBookingApp.Application.DTO;

namespace ResortBookingApp.Application.ServiceContracts
{
    public interface IAmenityService
    {
        Task<AmenityResponse> AddAmenity(AmenityAddRequest? amenity);

        Task<List<AmenityResponse>> GetAmenities();

        Task<AmenityResponse?> GetAmenity(int? id);

        Task<AmenityResponse> UpdateAmenity(AmenityUpdateRequest? amenity);

        Task<bool> DeleteAmenity(int? id);

        Task<bool> AmenityExists(int? id);
    }
}
