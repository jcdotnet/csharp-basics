using eCommerce.Domain.Entities;

namespace eCommerce.Application.RepositoryContracts
{
    public interface IUsersRepository
    {
        Task<ApplicationUser?> AddUser(ApplicationUser user);

        Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string? password);
    }
}
