using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MKExpress.API.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException(ModelStateDictionary modelState, string message = "Validation Failed!") :
            base(message)
        {
            Errors = modelState.Where(
                (Func<KeyValuePair<string, ModelStateEntry>, bool>)(
                    x =>
                    {
                        var modelStateEntry = x.Value;
                        return modelStateEntry != null && modelStateEntry.Errors.Count > 0;
                    })).ToDictionary(
                (Func<KeyValuePair<string, ModelStateEntry>, string>)(kvp => kvp.Key),
                (Func<KeyValuePair<string, ModelStateEntry>, List<string>>)(
                    kvp =>
                    {
                        var modelStateEntry = kvp.Value;
                        return (modelStateEntry != null
                            ? modelStateEntry.Errors
                                .Select((Func<ModelError, string>)(x => x.ErrorMessage))
                                .ToList()
                            : null) ?? new List<string>();
                    }));
        }

        public string ErrorResponseType { get; set; } = "InvalidInput";
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}