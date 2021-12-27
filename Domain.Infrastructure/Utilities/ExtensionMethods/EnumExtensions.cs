using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class EnumExtensions
    {
        public static T GetValue<T>(this System.Enum enumeration)
        {
            var result = (T)Convert.ChangeType(enumeration, typeof(T));
            return result;
        }
       
        public static T GetAttribute<T>(this System.Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static string ToDisplayName(this System.Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.Name;
        }

        public static string ToDisplayShortName(this System.Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.ShortName;
        }

        public static string ToDisplayGroupName(this System.Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.GroupName;
        }

        public static string ToDisplayDescription(this System.Enum value)
        {
            var attribute = value.GetAttribute<DisplayAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
        public static string ToDescription(this System.Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static IDictionary<string, int> ToDisplayNameDictionary(this Type enumType)
        {
            return System.Enum.GetValues(enumType)
            .Cast<object>()
            .ToDictionary(v => ((System.Enum)v).ToDisplayName(), k => (int)k);
        }

        public static IDictionary<string, int> ToDisplayDescriptionDictionary(this Type enumType)
        {
            return System.Enum.GetValues(enumType)
            .Cast<object>()
            .ToDictionary(v => ((System.Enum)v).ToDisplayDescription(), k => (int)k);
        }

        public static IDictionary<string, int> ToDescriptionDictionary(this Type enumType)
        {
            return System.Enum.GetValues(enumType)
            .Cast<object>()
            .ToDictionary(v => ((System.Enum)v).ToDescription(), k => (int)k);
        }

        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
           where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from Enum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e.GetHashCode(), Name = e.ToDescription() };
            return new SelectList(values, "Id", "Name", enumObj);
        }
    }
}
