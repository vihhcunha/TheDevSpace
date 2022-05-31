namespace TheDevSpace.Application.ValidationService;

public class ValidationService : IValidationService
{
    private List<ValidationData> _validationData;
    public IReadOnlyCollection<ValidationData> ValidationCollection => _validationData;

    public bool HasError => _validationData.Count > 0;

    public ValidationService()
    {
        _validationData = new List<ValidationData>();
    }

    public void AddError(string errorMessage)
    {
        var errorData = new ValidationData(errorMessage);
        _validationData.Add(errorData);
    }

    public void AddError(string errorMessage, string description)
    {
        var errorData = new ValidationData(errorMessage, description);
        _validationData.Add(errorData);
    }

    public void AddError(string errorMessage, string description, string field)
    {
        var errorData = new ValidationData(errorMessage, description, field);
        _validationData.Add(errorData);
    }
}
