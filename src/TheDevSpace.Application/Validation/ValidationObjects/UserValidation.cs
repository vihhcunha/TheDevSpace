using FluentValidation;

namespace TheDevSpace.Application.Validation;

public class UserValidation : AbstractValidator<UserDto>
{
    public UserValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("The name must be filled");

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("The e-mail must be filled");

        RuleFor(a => a.Password)
            .NotEmpty().WithMessage("The password must be filled");
    }
}