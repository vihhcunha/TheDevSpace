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
                var error = item.Description.IsNullOrEmpty() ? item.Error : $"{item.Error} - {item.Description}";
                modelState.AddModelError(item.Field.IsNullOrEmpty() ? string.Empty : item.Field, error);
            }
        }
    }
}
