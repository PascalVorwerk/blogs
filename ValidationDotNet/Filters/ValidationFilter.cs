using System.ComponentModel.DataAnnotations;

namespace ValidationDotNet.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argument = context.Arguments.OfType<T>().FirstOrDefault();
        
        if (argument == null)
        {
            return Results.BadRequest("Invalid request payload");
        }
        
        var validationContext = new ValidationContext(argument);
        var validationResults = new List<ValidationResult>();
        
        // This call validates all properties of the object, or only the required ones if 'ValidateAllProperties' is set to false
        Validator.TryValidateObject(argument, validationContext, validationResults, true);
        
        // We haven't checked if the custom rules are valid yet, so we need to do that as well
        if (argument is IValidatableObject validatableObject)
        {
            var customValidationResults = validatableObject.Validate(validationContext);
            validationResults.AddRange(customValidationResults);
        }

        // If there are no validation results, we can continue in the pipeline
        if (validationResults.Count == 0) return await next(context);
        
        var errorDict = new Dictionary<string, List<string>>();

        foreach (var result in validationResults)
        {
            var memberNames = result.MemberNames.Any() ? result.MemberNames : [string.Empty];

            foreach (var memberName in memberNames)
            {
                if (!errorDict.ContainsKey(memberName)) errorDict[memberName] = [];
                
                errorDict[memberName].Add(result.ErrorMessage ?? "Validation error");
            }
        }
        
        var finalErrors = errorDict.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToArray()
        );
        
        return Results.ValidationProblem(finalErrors);
    }
}