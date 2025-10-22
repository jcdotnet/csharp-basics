using Microsoft.AspNetCore.Identity;

namespace ContactsManager.Core.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Name { get; set; }
    }
}
