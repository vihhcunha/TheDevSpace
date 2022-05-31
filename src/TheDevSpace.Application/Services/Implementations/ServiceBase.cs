using FluentValidation;
using FluentValidation.Results;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Domain.Entities;

namespace TheDevSpace.Application;

public class ServiceBase
{
    private readonly IValidationService _validationService;

    protected ServiceBase(IValidationService validationService)
    {
        _validationService = validationService;
    }

    protected bool ExecuteValidation<TValidation, TEntity>(TValidation validation, TEntity entity) where TValidation : AbstractValidator<TEntity> where TEntity : class
    {
        var validator = validation.Validate(entity);

        if (validator.IsValid) return true;

        Notificate(validator);
        return false;
    }

    protected void Notificate(ValidationResult validator)
    {
        foreach (var error in validator.Errors)
        {
            Notificate(error.ErrorMessage, error.PropertyName);
        }
    }

    protected void Notificate(string error, string field)
    {
        _validationService.AddError(error, field);
    }

    protected void Notificate(string error)
    {
        _validationService.AddError(error);
    }
}
