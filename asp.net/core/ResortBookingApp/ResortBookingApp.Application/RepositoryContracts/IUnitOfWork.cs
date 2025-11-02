namespace ResortBookingApp.Application.RepositoryContracts
{
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
    }
}
