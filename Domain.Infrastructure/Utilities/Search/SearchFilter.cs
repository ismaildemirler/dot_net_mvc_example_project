using System;
using System.Linq.Expressions;

namespace Domain.Infrastructure.Utilities.Search
{
    public class SearchFilter
    {
        public string PropertyName { get; set; }
        public Op Operation { get; set; }
        public object Value { get; set; }


        /// <summary>
        /// property ismini getirir
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> property)
        {
        
            var a = property.Body.ToString().Split('.');
            var name = "";
            
                for (var i = 1; i < a.Length; i++)
                {
                    name +=  string.Format(".{0}", a[i]);
                }

            return name.Substring(1);


        }
    }

}