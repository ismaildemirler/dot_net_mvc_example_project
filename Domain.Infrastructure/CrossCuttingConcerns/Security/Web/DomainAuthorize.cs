using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Domain.Infrastructure.CrossCuttingConcerns.Security.Web
{
    public class DomainAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                httpContext.Response.Redirect("~/Account/login");
            }

            var roles = Roles.Split(',');

            var currentUserTypeId = CurrentUser.Identity.UserTypeId;

            
            if (currentUserTypeId == (byte)EnumSystemUserType.Admin)
            {
                if (roles.Contains("Admin"))
                    return true;
            }
            else if (currentUserTypeId == (byte)EnumSystemUserType.Customer)
            {
                if (roles.Contains("Customer"))
                    return true;
            }
            return base.AuthorizeCore(httpContext);
        }
    }

    public enum EnumSystemUserType
    {
        Customer = 0,
        Admin = 1
    }

}
