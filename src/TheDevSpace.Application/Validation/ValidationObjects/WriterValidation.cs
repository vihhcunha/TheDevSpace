using FluentValidation;

namespace TheDevSpace.Application.Validation;

public class WriterValidation : AbstractValidator<WriterDto>
{
    public WriterValidation()
    {
        RuleFor(a => a.Description)
            .NotEmpty().WithMessage("The description must be filled");

        RuleFor(a => a.Role)
            .NotEmpty().WithMessage("The role must be filled");
    }
}