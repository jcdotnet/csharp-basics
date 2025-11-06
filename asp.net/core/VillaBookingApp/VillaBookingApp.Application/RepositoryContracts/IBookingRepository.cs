using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task UpdateAsync(Booking booking);
    }
}
