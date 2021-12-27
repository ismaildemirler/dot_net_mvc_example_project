using System;

namespace Domain.Infrastructure.CrossCuttingConcerns.Validation.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class IArgumentValidationAttribute : Attribute
    {
        public abstract void ValidateArgument(object value, string argumentName, string gercekisim);
    }
}
