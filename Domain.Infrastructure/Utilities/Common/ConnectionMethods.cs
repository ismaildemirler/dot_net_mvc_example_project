using System.Linq;
using System.Web;

namespace Domain.Infrastructure.Utilities.Common
{
    public class ConnectionMethods
    {
        public static string GetUserIP()
        {
            string ip="::1";
            if (Utilities.IsWebApp)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["cc-ip"]))
                    ip = HttpContext.Current.Request.Headers["cc-ip"];
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["X-Forwarded-For"]))
                    ip = HttpContext.Current.Request.ServerVariables["X-Forwarded-For"];
                else
                {
                    ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                          && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                        ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                        : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (ip.Contains(","))
                    ip = ip.Split(',').First().Trim();

            }
            return ip;
        }
    }
}
