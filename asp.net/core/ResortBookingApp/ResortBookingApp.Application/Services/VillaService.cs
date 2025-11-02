using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Application.ServiceContracts;
using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.Services
{
    public class VillaService : IVillaService
    {
        private IVillaRepository _repository;

        public VillaService(IVillaRepository repository)
        {
            _repository = repository;
        }

        public async Task<VillaResponse> AddVilla(VillaAddRequest? villaAddRequest)
        {
            if (villaAddRequest is not null)
            {
                var villa = await _repository.AddAsync(villaAddRequest.ToVilla());
               
                return villa.ToVillaResponse();
            }
            throw new ArgumentNullException(nameof(villaAddRequest));
        }

        public async Task<VillaResponse?> GetVilla(int? villaId)
        {
            Villa? villa = await _repository.GetAsync(v => v.Id == villaId);

            if (villa is null) return null;

            return villa.ToVillaResponse();
        }

        public async Task<List<VillaResponse>> GetVillas()
        {
            var villas = await _repository.GetAllAsync();

            return villas.Select(v => v.ToVillaResponse()).ToList();
        }

        public async Task<VillaResponse> UpdateVilla(VillaUpdateRequest? villaUpdateRequest)
        {
            if (villaUpdateRequest is null)
            {
                throw new ArgumentNullException(nameof(villaUpdateRequest));
            }
            var villa = villaUpdateRequest.ToVilla();
            await _repository.UpdateAsync(villa);
            return villa.ToVillaResponse();
        }

        public async Task<bool> DeleteVilla(int? villaId)
        {
            if (villaId is null) throw new ArgumentNullException(nameof(villaId));

            Villa? villa = await _repository.GetAsync(v => v.Id == villaId);

            if (villa is null) { return false; }

            await _repository.RemoveAsync(villa);
            return true;
        }
    }
}
