
using FluentValidation.Results;

namespace APIFacturaV1.Extensions
{
    public static class ValidatorExtension
    {
        public static Dictionary<string, string[]> ToValidationProblems(this ValidationResult result)
        {
            return result.Errors.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
                  .ToDictionary(x => x.Key, x => x.ToArray());
        }
    }
}
