using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity_Web.Areas.Admin.Models.DTOs.Roles
{
    public class AddUserRoleDto
    {
        public string Id { get; set; }=string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<SelectListItem> Roles { get; set; }

    }
}
