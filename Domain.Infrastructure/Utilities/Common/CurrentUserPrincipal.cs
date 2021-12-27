using Domain.Infrastructure.CrossCuttingConcerns.Security;
using System.Web;

namespace Domain.Infrastructure.Utilities.Common
{
    public class CurrentUserPrincipal
    {
        public static DomainMYSIdentity Identity
        {
            get
            {
                if (!Utilities.IsWebApp || (
                    HttpContext.Current.User == null ||
                    HttpContext.Current.User.Identity.IsAuthenticated == false))
                    return new DomainMYSIdentity();

                var identity = (DomainMYSIdentity)HttpContext.Current.User.Identity;
                if (identity != null)
                    return identity;
                return new DomainMYSIdentity();
            }
        }
    }
}
