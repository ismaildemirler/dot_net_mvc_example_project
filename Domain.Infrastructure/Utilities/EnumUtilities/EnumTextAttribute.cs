using System;

namespace Domain.Infrastructure.Utilities.EnumUtilities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumTextAttribute : Attribute
    {
        public string Text { get; set; }
    }
}
