namespace eCommerce.Application.DTO
{
    public record RegisterRequest // using a record rather than a class
    (
        string? Email, string? Password, string? UserName, GenderOptions Gender
    );
    
}
