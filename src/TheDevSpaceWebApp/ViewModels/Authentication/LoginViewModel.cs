using System.ComponentModel.DataAnnotations;

namespace TheDevSpaceWebApp.ViewModels.Authentication
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mail is mandatory!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory!")]
        public string Password { get; set; }
    }
}
