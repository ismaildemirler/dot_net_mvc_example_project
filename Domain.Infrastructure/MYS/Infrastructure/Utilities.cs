using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace Domain.Infrastructure.MYS.Infrastructure
{
    public class Utilities
    {
        //public static List<EnumCouncilDecisionSubject> assetEnteredSubjects = new List<EnumCouncilDecisionSubject>
        //{
        //    EnumCouncilDecisionSubject.ProjeBasvurusununAySureiledesteklenmesine,
        //    EnumCouncilDecisionSubject.ProjeSuresininAyUzatilmasina,
        //    EnumCouncilDecisionSubject.ProjeKonusuDesteklerinRevize,
        //    EnumCouncilDecisionSubject.RevizyonTalebininReddedilmesine
        //};

    internal static string JSONSerialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Error = (s, e) => { e.ErrorContext.Handled = true; }
                });
        }
        public static string RandomString(int length)
        {
            var random = new Random();
            Thread.Sleep(10);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string BenzersizAnahtarOlustur()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper() + RandomString(10);
        }
        public static string GetIp()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["cc-ip"]))
                    return HttpContext.Current.Request.Headers["cc-ip"];
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                return !string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress) ? HttpContext.Current.Request.UserHostAddress : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string TextCensor(string text, char censorChar = '*')
        {
            if (string.IsNullOrEmpty(text)) return "";
            text = text.Trim();
            if (text.Length == 0) return "";

            var strArray = text.ToCharArray();
            for (var i = 0; i < strArray.Length; i++)
            {
                if (i % 2 != 0)
                    strArray[i] = censorChar;
            }

            return new string(strArray);
        }
        public static bool IsWebApp => HttpContext.Current?.Session != null;
        public static Guid GetAppId()
        {
            return Guid.Parse(ConfigurationManager.AppSettings["DomainMYSAppId"]);
        }
        public static string Browsergetir()
        {
            return HttpContext.Current.Request.Browser.Browser;
        }
        public static string Osgetir()
        {

            var os = Environment.OSVersion;
            var userAgent = HttpContext.Current.Request.UserAgent;

            if (userAgent.IndexOf("Windows NT 6.3", StringComparison.Ordinal) > 0)
            {
                return "Windows 8.1";
            }
            if (userAgent.IndexOf("Windows NT 6.2", StringComparison.Ordinal) > 0)
            {
                return "Windows 8";
            }
            if (userAgent.IndexOf("Windows NT 6.1", StringComparison.Ordinal) > 0)
            {
                return "Windows 7";
            }
            if (userAgent.IndexOf("Windows NT 6.0", StringComparison.Ordinal) > 0)
            {
                return "Windows Vista";
            }
            if (userAgent.IndexOf("Windows NT 5.2", StringComparison.Ordinal) > 0)
            {
                return "Windows Server 2003; Windows XP x64 Edition";
            }
            if (userAgent.IndexOf("Windows NT 5.1", StringComparison.Ordinal) > 0)
            {
                return "Windows XP";
            }
            if (userAgent.IndexOf("Windows NT 5.01", StringComparison.Ordinal) > 0)
            {
                return "Windows 2000, Service Pack 1 (SP1)";
            }
            if (userAgent.IndexOf("Windows NT 5.0", StringComparison.Ordinal) > 0)
            {
                return "Windows 2000";
            }
            if (userAgent.IndexOf("Windows NT 4.0", StringComparison.Ordinal) > 0)
            {
                return "Microsoft Windows NT 4.0";
            }
            if (userAgent.IndexOf("Win 9x 4.90", StringComparison.Ordinal) > 0)
            {
                return "Windows Millennium Edition (Windows Me)";
            }
            if (userAgent.IndexOf("Windows 98", StringComparison.Ordinal) > 0)
            {
                return "Windows 98";
            }
            if (userAgent.IndexOf("Windows 95", StringComparison.Ordinal) > 0)
            {
                return "Windows 95";
            }
            if (userAgent.IndexOf("Windows CE", StringComparison.Ordinal) > 0)
            {
                return "Windows CE";
            }
            return "Others";
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
}
