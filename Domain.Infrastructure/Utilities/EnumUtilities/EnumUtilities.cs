using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Domain.Infrastructure.Utilities.EnumUtilities
{
    public class EnumUtilities
    {
        public static List<EnumItem> GetEnumList(Type enumType)
        {
            var list = new List<EnumItem>();

            var enumValues = Enum.GetValues(enumType);

            foreach (var value in enumValues)
            {
                var attribute = (EnumTextAttribute)enumType.GetMember(value.ToString())[0].GetCustomAttributes(typeof(EnumTextAttribute), false).FirstOrDefault();
                var text = attribute != null ? attribute.Text : value.ToString();

                list.Add(new EnumItem
                {
                    Text = text,
                    Value = value.GetHashCode()
                });
            }

            return list;
        }
    }
}
