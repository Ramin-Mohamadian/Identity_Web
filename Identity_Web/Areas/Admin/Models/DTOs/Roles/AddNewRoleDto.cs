using System.ComponentModel.DataAnnotations;

namespace Identity_Web.Areas.Admin.Models.DTOs.Roles
{
    public class AddNewRoleDto
    {
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        public string Description { get; set; }=string.Empty ;
    }
}
