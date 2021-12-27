using System;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Domain.Infrastructure.CrossCuttingConcerns.Security.Web
{
    public class AuthenticationHelper
    {
        public static void CreateAuthCookie(Guid Id, string userName, string eMail, DateTime expiration, string[] roles, bool rememberMe, string firstName, string lastName)
        {
            var authTicket = new FormsAuthenticationTicket(1, userName, DateTime.Now, expiration, rememberMe, CreateAuthTags(eMail, firstName, lastName, Id));

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }

        private static string CreateAuthTags(string eMail, string firstName, object lastName, Guid id)
        {
            var sb = new StringBuilder();
            sb.Append(eMail);
            sb.Append("|");
            sb.Append(firstName);
            sb.Append("|");
            sb.Append(lastName);
            sb.Append("|");
            sb.Append(id);
            return sb.ToString();
        }
    }
}
