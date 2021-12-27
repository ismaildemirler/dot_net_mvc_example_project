using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using FluentValidation;
using Domain.Infrastructure.Utilities.Exception;
using System.Linq;

namespace Domain.Infrastructure.CrossCuttingConcerns.Validation.FluentValidation
{
    public class ValidatorTool
    {
        public static void FluentValidate(IValidator validator, object entity)
        {
            var result = validator.Validate(entity);
            string hata = "";
            foreach (var error in result.Errors)
            {
                hata += $"{error.ErrorMessage}" + ",";
            }
            if (result.Errors.Any())
            {
                throw new ValidationCoreException(ExceptionUtility.FormattedException(hata));
            }
        }
    }
}
