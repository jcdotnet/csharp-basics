using VillaBookingApp.Application.DTO;
using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.Services
{
    public class VillaService : IVillaService
    {
        private IUnitOfWork _unitOfWork;

        public VillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VillaResponse> AddVilla(VillaAddRequest? villaAddRequest)
        {
            if (villaAddRequest is not null)
            {
                var villa = await _unitOfWork.Villa.AddAsync(villaAddRequest.ToVilla());
               
                return villa.ToVillaResponse();
            }
            throw new ArgumentNullException(nameof(villaAddRequest));
        }

        public async Task<VillaResponse?> GetVilla(int? villaId)
        {
            Villa? villa = await _unitOfWork.Villa.GetAsync(v => v.Id == villaId, "Amenities");

            if (villa is null) return null;

            return villa.ToVillaResponse();
        }

        public async Task<List<VillaResponse>> GetVillas()
        {
            var villas = await _unitOfWork.Villa.GetAllAsync(includeProperties: "Amenities");

            return villas.Select(v => v.ToVillaResponse()).ToList();
        }

        public async Task<VillaResponse> UpdateVilla(VillaUpdateRequest? villaUpdateRequest)
        {
            if (villaUpdateRequest is null)
            {
                throw new ArgumentNullException(nameof(villaUpdateRequest));
            }
            var villa = villaUpdateRequest.ToVilla();
            await _unitOfWork.Villa.UpdateAsync(villa);
            return villa.ToVillaResponse();
        }

        public async Task<bool> DeleteVilla(int? villaId)
        {
            if (villaId is null) throw new ArgumentNullException(nameof(villaId));

            Villa? villa = await _unitOfWork.Villa.GetAsync(v => v.Id == villaId);

            if (villa is null) { return false; }

            await _unitOfWork.Villa.RemoveAsync(villa);
            return true;
        }
    }
}
