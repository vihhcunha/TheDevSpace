using FluentValidation;

namespace TheDevSpace.Application.Validation;

public class UserUpdateValidation : AbstractValidator<UserDto>
{
    public UserUpdateValidation()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("The name must be filled");

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("The e-mail must be filled");
    }
}