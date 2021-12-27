using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class ConvertExtensionMethods
    {
        public static int ToInt32(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static double ToDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static decimal ToDecimal(this object obj)
        {
            return Convert.ToDecimal(obj);
        }

        public static bool ToBoolean(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static long ToLong(this object obj)
        {
            return Convert.ToInt64(obj);
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static List<T> ToList<T>(this string value, char split = ',')
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return (from veri in value.Split(split) where !string.IsNullOrEmpty(veri) select (T)Convert.ChangeType(veri, typeof(T))).ToList<T>();
        }
        public static string ToString(this List<int> value, char split = ',')
        {
            var birlestirilenString = "";
            foreach (var deger in value)
            {
                birlestirilenString = deger + ",";
            }

            if (birlestirilenString.Length > 0)
                birlestirilenString = birlestirilenString.Remove(birlestirilenString.Length - 1, 1);

            return birlestirilenString;
        }
        public static double ToCurrencyToDouble(this object obj) 
        {
            return Convert.ToDouble(obj.ToString().Replace(".", string.Empty).Replace("_", string.Empty));
        }
    }
}
