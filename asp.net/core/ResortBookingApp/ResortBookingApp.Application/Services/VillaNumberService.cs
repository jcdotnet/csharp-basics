using Microsoft.EntityFrameworkCore;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;
using ResortBookingApp.Infrastructure.Data;

namespace ResortBookingApp.Application.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberService(ApplicationDbContext db)
        {
            _db = db; 
        }

        public async Task<VillaNumberResponse> AddVillaNumber(VillaNumberAddRequest? addRequest)
        {
            if (addRequest is not null)
            {
                var villaNumber = addRequest.ToVillaNumber();
                _db.VillaNumbers.Add(villaNumber);
                await _db.SaveChangesAsync();
                return villaNumber.ToVillaNumberResponse();
            }
            throw new ArgumentNullException(nameof(addRequest));
        }

        public async Task<List<VillaNumberResponse>> GetVillaNumbers()
        {
            return await _db.VillaNumbers.Include(v=> v.Villa)
                .Select(v => v.ToVillaNumberResponse()).ToListAsync();
        }

        public async Task<VillaNumberResponse?> GetVillaNumber(int? number)
        {
            var villaNumber = await _db.VillaNumbers.Include(v => v.Villa).
                FirstOrDefaultAsync(v => v.Number == number);

            if (villaNumber is null) return null;

            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<VillaNumberResponse> UpdateVillaNumber(VillaNumberUpdateRequest? updateRequest)
        {
            if (updateRequest is null)
            {
                throw new ArgumentNullException(nameof(updateRequest));
            }
            var villaNumber = updateRequest.ToVillaNumber();
            _db.VillaNumbers.Update(villaNumber);
            await _db.SaveChangesAsync();
            return villaNumber.ToVillaNumberResponse();
        }

        public async Task<bool> DeleteVillaNumber(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            var villaNumber = await _db.VillaNumbers.FirstOrDefaultAsync(v => v.Number == number);

            if (villaNumber is null) { return false; }

            _db.VillaNumbers.Remove(villaNumber);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VillaNumberExists(int? number)
        {
            if (number is null) throw new ArgumentNullException(nameof(number));

            return await _db.VillaNumbers.AnyAsync(v => v.Number == number);
        }
    }
}
