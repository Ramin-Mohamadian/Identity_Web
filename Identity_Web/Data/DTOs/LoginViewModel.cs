using System.ComponentModel.DataAnnotations;

namespace Identity_Web.Data.DTOs
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }=string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }= string.Empty;

        public bool IsPersistant { get; set; } = false;
    }
}
