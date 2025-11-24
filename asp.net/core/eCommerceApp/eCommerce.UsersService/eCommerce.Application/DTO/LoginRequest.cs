namespace eCommerce.Application.DTO
{
    public record LoginRequest // using a record rather than a class
    (
        string? Email, string? Password
    );
}
