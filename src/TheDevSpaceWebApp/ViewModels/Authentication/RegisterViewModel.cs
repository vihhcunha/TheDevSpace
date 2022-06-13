using System.ComponentModel.DataAnnotations;

namespace TheDevSpaceWebApp.ViewModels.Authentication;

public class RegisterViewModel
{
    [Required(ErrorMessage = "You must set your name!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "You must set your e-mail!")]
    [EmailAddress(ErrorMessage = "You must set a valid e-mail!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "You must set your password!")]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Your passwords must match!")]
    public string ConfirmPassword { get; set; }
}
