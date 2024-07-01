using System.ComponentModel.DataAnnotations;

namespace Identity_Web.Data.DTOs
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string  FirstName { get; set; }=string.Empty;


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
