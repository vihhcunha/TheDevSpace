namespace TheDevSpaceWebApp.Models;

public class ErrorViewModel
{
    public int StatusCode { get; private set; }
    public string Message { get; private set; }
    public string Title { get; private set; }

    private ErrorViewModel(int statusCode, string title, string message)
    {
        StatusCode = statusCode;
        Title = title;
        Message = message;
    }

    public class ErrorViewModelBuilder
    {
        private static readonly Dictionary<int, Func<(string, string)>> ErrorMessageBuilder = new Dictionary<int, Func<(string, string)>>
        {
            { 500, () => BuildStatus500ErrorMessage() },
            { 401, () => BuildStatus401ErrorMessage() },
            { 404, () => BuildStatus404ErrorMessage() }
        };

        public static ErrorViewModel BuildViewModel(int statusCode)
        {
            var errorMessages = BuildStatus500ErrorMessage();

            if (ErrorMessageBuilder.ContainsKey(statusCode))
                errorMessages = ErrorMessageBuilder[statusCode].Invoke();

            return new ErrorViewModel(statusCode, errorMessages.Item1, errorMessages.Item2);
        }

        private static (string, string) BuildStatus404ErrorMessage()
        {
            return ("Oppsss. Not found!", "The resource that you were looking for does not exists!");
        }

        private static (string, string) BuildStatus401ErrorMessage()
        {
            return ("Oppsss. You can't access it!", "You don't have access to the resource that you were looking!");
        }

        private static (string, string) BuildStatus500ErrorMessage()
        {
            return ("Oppsss. Something went wrong!", "Try again later!");
        }
    }
}