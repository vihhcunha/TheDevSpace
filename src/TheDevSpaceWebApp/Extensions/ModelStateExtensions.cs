using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheDevSpace.Application.ValidationService;

namespace TheDevSpaceWebApp.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddValidationData(this ModelStateDictionary modelState, IReadOnlyCollection<ValidationData> validations)
        {
            foreach (var item in validations)
            {
                modelState.AddModelError(item.Field, $"{item.Error} - {item.Description}");
            }
        }
    }
}
