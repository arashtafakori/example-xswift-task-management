using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Doit.AccountModule.Presentation.WebAPI
{
    public class SupressValidationObjectModelValidator : IObjectModelValidator
    {
        public void Validate(ActionContext actionContext, ValidationStateDictionary validationState, string prefix, object model)
        {
            // Suppress validation by clearing the validation state
            validationState.Clear();
        }
    }
}
