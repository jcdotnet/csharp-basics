using eCommerce.Application.DTO;

namespace eCommerce.Application.ServiceContracts
{
    public interface IUsersService
    {
        Task<UserDto?> GetUser(Guid userId);

        Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

        Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);
    }
}
