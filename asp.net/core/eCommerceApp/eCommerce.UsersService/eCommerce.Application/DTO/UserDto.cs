namespace eCommerce.Application.DTO
{
    public record UserDto(Guid UserId, string? Email, string? UserName, string Gender)
    {
    }
}
