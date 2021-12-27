using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
   public static class RouteValueDictionaryExtensions
    {
        public static RouteValueDictionary Create(this List<int> parameterList, string paramterName)
        {
            var referenceTypes = parameterList;

            var parameters = new RouteValueDictionary();

            for (int i = 0; i < referenceTypes.Count; ++i)
            {
                parameters.Add(paramterName + "[" + i + "]", referenceTypes[i]);
            }

            return parameters;
        }

    }
}
