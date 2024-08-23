using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Bloggig.Presentation.Extensions;

public static class ModelStateExtension
{
    public static string GetFirstError(this ModelStateDictionary modelStateDictionary)
    {
        return modelStateDictionary.Values
            .SelectMany(values => values.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault();
    }
}
