using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.Extensions;

namespace TheDevSpaceWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IValidationService _validationService;

        protected BaseController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public bool IsInvalidOperation()
        {
            if (_validationService.HasError) ModelState.AddValidationData(_validationService.ValidationCollection);
            return _validationService.HasError;
        }
    }
}
