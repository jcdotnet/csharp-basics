namespace eCommerce.Domain.Entities
{
    public class ApplicationUser
    {
        // we don't use ASP.NET Core Identity: we will integrate with Azure B2C Auth later
        public Guid UserId { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? UserName { get; set; }

        public string? Gender { get; set; }
    }
}
