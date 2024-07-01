using Microsoft.AspNetCore.Identity;

namespace Identity_Web.Data.Entities
{
    public class User: IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
    }
}
