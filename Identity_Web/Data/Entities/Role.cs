using Microsoft.AspNetCore.Identity;

namespace Identity_Web.Data.Entities
{
    public class Role:IdentityRole
    {
        public string Description { get; set; } = string.Empty;
    }
}
