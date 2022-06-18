using System.ComponentModel.DataAnnotations;

namespace TheDevSpaceWebApp.ViewModels.Writer;

public class RegisterWriterViewModel
{
    public Guid WriterId { get; set; }

    [Required(ErrorMessage = "Your age is mandatory!")]
    public int Age { get; set; }

    [Required(ErrorMessage = "A description about you is mandatory!")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Your role is mandatory!")]
    public string Role { get; set; }
}
