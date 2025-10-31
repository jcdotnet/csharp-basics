using Microsoft.EntityFrameworkCore;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;
using ResortBookingApp.Domain.Entities;
using ResortBookingApp.Infrastructure.Data;

namespace ResortBookingApp.Application.Services
{
    public class VillaService : IVillaService
    {
        private readonly ApplicationDbContext _db;

        public VillaService(ApplicationDbContext db)
        {
            _db = db; 
        }

        public async Task<VillaResponse> AddVilla(VillaAddRequest? villaAddRequest)
        {
            if (villaAddRequest is not null)
            {
                var villa = villaAddRequest.ToVilla();
                _db.Villas.Add(villa);
                await _db.SaveChangesAsync();
                return villa.ToVillaResponse();
            }
            throw new ArgumentNullException(nameof(villaAddRequest));
        }

        public async Task<VillaResponse?> GetVilla(int? villaId)
        {
            Villa? villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == villaId);

            if (villa is null) return null;

            return villa.ToVillaResponse();
        }

        public async Task<List<VillaResponse>> GetVillas()
        { 
            return await _db.Villas.Select(v => v.ToVillaResponse()).ToListAsync();
        }

        public async Task<VillaResponse> UpdateVilla(VillaUpdateRequest? villaUpdateRequest)
        {
            if (villaUpdateRequest is null)
            {
                throw new ArgumentNullException(nameof(villaUpdateRequest));
            }
            var villa = villaUpdateRequest.ToVilla();
            _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
            return villa.ToVillaResponse();
        }

        public async Task<bool> DeleteVilla(int? villaId)
        {
            if (villaId is null) throw new ArgumentNullException(nameof(villaId));

            Villa? villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == villaId);

            if (villa is null) { return false; }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
