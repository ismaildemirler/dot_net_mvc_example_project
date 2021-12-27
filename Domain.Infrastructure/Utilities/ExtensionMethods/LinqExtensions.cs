using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class LinqExtensions
    {
        private static PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }


        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {

            var a = property.Body.ToString().Split('.');
            var name = "";

            for (var i = 1; i < a.Length; i++)
            {
                name += $".{a[i]}";
            }

            return name.Substring(1);
        }

        private static LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
    }
}
