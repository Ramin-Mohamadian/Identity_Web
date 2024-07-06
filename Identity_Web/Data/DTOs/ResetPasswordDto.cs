using System.ComponentModel.DataAnnotations;

namespace Identity_Web.Data.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }=string.Empty;
        [Required]
        public string Token { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; }=string.Empty;
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
