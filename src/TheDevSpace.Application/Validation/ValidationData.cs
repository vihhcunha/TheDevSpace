namespace TheDevSpace.Application.ValidationService;

public class ValidationData
{
    public string Error { get; private set; }
    public string Description { get; private set; }
    public string Field { get; private set; }

    public ValidationData(string error, string description, string field)
    {
        Error = error;
        Description = description;
        Field = field;

        Validate();
    }

    public ValidationData(string error, string description)
    {
        Error = error;
        Description = description;

        Validate();
    }

    public ValidationData(string error)
    {
        Error = error;

        Validate();
    }

    private void Validate()
    {
        if (String.IsNullOrEmpty(Error)) throw new ArgumentNullException(nameof(Error));
    }
}
