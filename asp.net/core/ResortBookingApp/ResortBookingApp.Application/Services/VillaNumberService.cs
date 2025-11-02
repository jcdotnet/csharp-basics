using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.RepositoryContracts;
using ResortBookingApp.Application.ServiceContracts;

namespace ResortBookingApp.Application.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private IVillaNumberRepository _repository;

        public VillaNumberService(IVillaNumberRepository repository)
        {
            _repository = repository;
        }

        public async Task<VillaNumberResponse> AddVillaNumber(VillaNumberAddRequest? addRequest)
        {
            if (addRequest is not null)
            {
                var villaNumber = await _repository.AddAsync(addRequest.ToVillaNumber());
                return villaNumber.ToVillaNumberResponse();
            }
            throw new ArgumentNullException(nameof(addRequest));
        }

        public async Task<List<VillaNumberResponse>> GetVillaNumbers()
        {
            return (await _repository.GetAllAsync(null, "Villa"))
                .Select(v => v.ToVillaNumberResponse()).ToList();
        }

        public async Task<VillaNumberResponse?> GetVillaNumber(int? number)
        {
            var villaNumber = await _repository.GetAsync(v => v.Number == number, "Villa");

            if (villaNumber is null) return null;

            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<VillaNumberResponse> UpdateVillaNumber(VillaNumberUpdateRequest? updateRequest)
        {
            if (updateRequest is null) throw new ArgumentNullException(nameof(updateRequest));
            
            var villaNumber = updateRequest.ToVillaNumber();
            await _repository.UpdateAsync(villaNumber);

            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<bool> DeleteVillaNumber(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            var villaNumber = await _repository.GetAsync(v => v.Number == number, "Villa");

            if (villaNumber is null) { return false; }

            await _repository.RemoveAsync(villaNumber);
            return true;
        }

        public async Task<bool> VillaNumberExists(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            return await _repository.AnyAsync(v => v.Number == number);
        }
    }
}
