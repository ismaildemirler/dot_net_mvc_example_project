using Domain.Infrastructure.CrossCuttingConcerns.Validation.ValidationAttributes;
using Ninject.Extensions.Interception;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Domain.Infrastructure.CrossCuttingConcerns.Validation.EnterpriseLibrary
{
    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            //Burada invocation önemli, getcustomattributes, reflectionlar ve bir sınıfa proje ismini bulup erişme konuları çok önemli burada.
            HttpContext.Current.Session["Validation"] = null;
            HttpContext.Current.Session["Alanlar"] = null;
            var parameters = invocation.Request.Method.GetParameters();
            var m = invocation.Request.Method.GetCustomAttributes(true);
            if (parameters.Any())
            {
                if (((parameters[0]).Member).Name == "Insert" || ((parameters[0]).Member).Name == "Update")
                {
                    for (int index = 0; index < parameters.Length; index++)
                    {
                        var sinifadi = parameters[index];
                        string siniftipi = (sinifadi.ParameterType).AssemblyQualifiedName;
                        Object sinif = (Activator.CreateInstance(Type.GetType(siniftipi)));
                        IEnumerable<PropertyInfo> methods = sinif.GetType().GetProperties();

                        foreach (PropertyInfo method in methods)
                        {
                            IArgumentValidationAttribute[] sonuc = (IArgumentValidationAttribute[])method.GetCustomAttributes(typeof(IArgumentValidationAttribute), true);

                            Object value = method.GetValue(invocation.Request.Arguments[index]);

                            Attribute name = method.GetCustomAttribute(typeof(DisplayAttribute));

                            foreach (IArgumentValidationAttribute attr in sonuc)
                            {
                                attr.ValidateArgument(value, ((DisplayAttribute)(name)).Name, method.Name);
                            }
                        }
                    }

                    if (HttpContext.Current.Session["Validation"] != null)
                    {
                        throw new ArgumentException(HttpContext.Current.Session["Validation"].ToString(), new Exception(HttpContext.Current.Session["Alanlar"].ToString()));
                    }
                }
            }

            invocation.Proceed();

            //foreach (IArgumentValidationAttribute attr in invocation.Request.Method.ReturnTypeCustomAttributes.GetCustomAttributes(typeof(IArgumentValidationAttribute), true))
            //{
            //    attr.ValidateArgument(invocation.ReturnValue, "return value");
            //}
        }
    }
}
