namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IAmenityRepository Amenity { get; }
        IBookingRepository Booking { get; }
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        Task SaveAsync();
    }
}
