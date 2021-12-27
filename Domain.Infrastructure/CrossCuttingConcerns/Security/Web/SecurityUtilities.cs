using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace Domain.Infrastructure.CrossCuttingConcerns.Security.Web
{
    public class SecurityUtilities
    {
        public DomainMYSIdentity FormsAuthTicketToIdentity(FormsAuthenticationTicket ticket)
        {
            return JsonConvert.DeserializeObject<DomainMYSIdentity>(ticket.UserData);
        }


        public static void SetCookieToPrincipal()
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                var encTicket = authCookie?.Value;
                if (string.IsNullOrEmpty(encTicket)) return;

                var ticket = FormsAuthentication.Decrypt(encTicket);

                var identity = JsonConvert.DeserializeObject<DomainMYSIdentity>(ticket.UserData);

                var principal = new GenericPrincipal(identity, new string[0]);

                HttpContext.Current.User = principal;
                Thread.CurrentPrincipal = principal;

            }
            catch (Exception)
            {

            }
        }
    }
    public class StringProtector
    {
        readonly string Purpose = ConfigurationManager.AppSettings["CryptoPassPhrase"];

        public string Protect(string unprotectedText)
        {
            var unprotectedBytes = Encoding.UTF8.GetBytes(unprotectedText);
            var protectedBytes = MachineKey.Protect(unprotectedBytes, Purpose);
            var protectedText = Convert.ToBase64String(protectedBytes);
            return protectedText;
        }

        public string Unprotect(string protectedText)
        {
            var protectedBytes = Convert.FromBase64String(protectedText);
            var unprotectedBytes = MachineKey.Unprotect(protectedBytes, Purpose);
            var unprotectedText = Encoding.UTF8.GetString(unprotectedBytes);
            return unprotectedText;
        }
    }
}
