using FluentValidation;

namespace TheDevSpace.Application.Validation;

public class ArticleValidation : AbstractValidator<ArticleDto>
{
    public ArticleValidation()
    {
        RuleFor(a => a.Title)
            .NotEmpty().WithMessage("The title must be filled");

        RuleFor(a => a.Content)
            .NotEmpty().WithMessage("The content must be filled");

        RuleFor(a => a.WriterId)
            .NotEqual(a => Guid.Empty).WithMessage("The writer must be filled");
    }
}

