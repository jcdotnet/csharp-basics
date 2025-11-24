namespace eCommerce.Application.DTO
{
    public record AuthenticationResponse
    (
        Guid UserId,
        string? Email,
        string? UserName,
        string? Gender,
        string? Token,
        bool Success
    )
    {
        // parameterless constructor in order for the AutoMapper to work
        public AuthenticationResponse() : this(default, default, default, default, default, default)
        {       
        }
    }
}
