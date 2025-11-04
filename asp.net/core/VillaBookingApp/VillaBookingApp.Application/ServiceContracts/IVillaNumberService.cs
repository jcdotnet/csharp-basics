using VillaBookingApp.Application.DTO;

namespace VillaBookingApp.Application.ServiceContracts
{
    public interface IVillaNumberService
    {
        Task<VillaNumberResponse> AddVillaNumber(VillaNumberAddRequest? villaNumber);

        Task<List<VillaNumberResponse>> GetVillaNumbers();

        Task<VillaNumberResponse?> GetVillaNumber(int? number);

        Task<VillaNumberResponse> UpdateVillaNumber(VillaNumberUpdateRequest? villaNumber);

        Task<bool> DeleteVillaNumber(int? number);

        Task<bool> VillaNumberExists(int? number);
    }
}
