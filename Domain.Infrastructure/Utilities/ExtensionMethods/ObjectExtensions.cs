using System;
using System.Web.Security;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static string Encrypt(this object value)
        {
            return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                string.Empty,
                DateTime.Now,
                DateTime.Now.AddMinutes(20160),
                true,
                value.ToString()));
        }
    }
}
