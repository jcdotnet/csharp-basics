using ResortBookingApp.Application.DTO;

namespace ResortBookingApp.Application.ServiceContracts
{
    public interface IVillaService
    {
        Task<VillaResponse> AddVilla(VillaAddRequest? villa);

        Task<List<VillaResponse>> GetVillas();

        Task<VillaResponse?> GetVilla(int? villaId);

        Task<VillaResponse> UpdateVilla(VillaUpdateRequest? villa);

        Task<bool> DeleteVilla(int? villaId);
    }
}
