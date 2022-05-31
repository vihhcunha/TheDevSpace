namespace TheDevSpace.Application.ValidationService;

public interface IValidationService
{
    IReadOnlyCollection<ValidationData> ValidationCollection { get; }
    bool HasError { get; }
    void AddError(string errorMessage);
    void AddError(string errorMessage, string description);
    void AddError(string errorMessage, string description, string field);
}
