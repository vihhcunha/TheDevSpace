using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheDevSpaceWebApp.ViewModels.Authentication;

public class UserSettingsViewModel
{
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Your name is mandatory!")]
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }

    [DisplayName("Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Your passwords must match!")]
    public string? ConfirmPassword { get; set; }
    public bool IsWriter { get; set; }
    public Guid? WriterId { get; set; }
    public int? Age { get; set; }
    public string? Description { get; set; }
    public string? Role { get; set; }
}
