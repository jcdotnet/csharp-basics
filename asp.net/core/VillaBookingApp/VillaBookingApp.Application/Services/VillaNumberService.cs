using VillaBookingApp.Application.DTO;
using VillaBookingApp.Application.RepositoryContracts;
using VillaBookingApp.Application.ServiceContracts;

namespace VillaBookingApp.Application.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private IUnitOfWork _unitOfWork;

        public VillaNumberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VillaNumberResponse> AddVillaNumber(VillaNumberAddRequest? addRequest)
        {
            if (addRequest is not null)
            {
                var villaNumber = await _unitOfWork.VillaNumber.AddAsync(addRequest.ToVillaNumber());
                return villaNumber.ToVillaNumberResponse();
            }
            throw new ArgumentNullException(nameof(addRequest));
        }

        public async Task<List<VillaNumberResponse>> GetVillaNumbers()
        {
            return (await _unitOfWork.VillaNumber.GetAllAsync(includeProperties: "Villa"))
                .Select(v => v.ToVillaNumberResponse()).ToList();
        }

        public async Task<VillaNumberResponse?> GetVillaNumber(int? number)
        {
            var villaNumber = await _unitOfWork.VillaNumber.GetAsync(v => v.Number == number, "Villa");

            if (villaNumber is null) return null;

            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<VillaNumberResponse> UpdateVillaNumber(VillaNumberUpdateRequest? updateRequest)
        {
            if (updateRequest is null) throw new ArgumentNullException(nameof(updateRequest));
            
            var villaNumber = updateRequest.ToVillaNumber();
            await _unitOfWork.VillaNumber.UpdateAsync(villaNumber);

            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<bool> DeleteVillaNumber(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            var villaNumber = await _unitOfWork.VillaNumber.GetAsync(v => v.Number == number, "Villa");

            if (villaNumber is null) { return false; }

            await _unitOfWork.VillaNumber.RemoveAsync(villaNumber);
            return true;
        }

        public async Task<bool> VillaNumberExists(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            return await _unitOfWork.VillaNumber.AnyAsync(v => v.Number == number);
        }
    }
}
