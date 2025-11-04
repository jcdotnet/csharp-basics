namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IAmenityRepository Amenity { get; }
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
    }
}
