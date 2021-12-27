using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Domain.Infrastructure.MYS.Infrastructure.Web;
using Domain.Infrastructure.Web;
using System;
using System.Collections.Generic;
using System.Web;

namespace Domain.Infrastructure.MYS.Infrastructure
{
    public class CurrentUser
    {
        public static bool IsAuthenticated { get { return CurrentUser.Identity != null && CurrentUser.Identity.UserId > 0; } }

        public static DomainMYSIdentity Identity
        {
            get
            {
                if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
                    return new DomainMYSIdentity();
                return (DomainMYSIdentity)HttpContext.Current.User.Identity;
            }
        }

        //public static List<Views> Views
        //{
        //    get
        //    {
        //        if (SessionManager.Get($"{UserLoginGuid}{Utilities.GetAppId()}_Views") == null)
        //        {
        //            if (Identity.IsAuthenticated)
        //            {
        //                var client = MYSWcfProxy<ISystemUserService>.CreateChannel();
        //                var response = client.GetSystemUserViews(new RequestSystemUser
        //                {
        //                    SystemUserId = Identity.UserId,
        //                    AppId = Utilities.GetAppId()
        //                }).ToList();
        //                SessionManager.Add(response, $"{UserLoginGuid}{Utilities.GetAppId()}_Views");
        //            }
        //            else
        //                SessionManager.Add(new List<Views>(), $"{UserLoginGuid}{Utilities.GetAppId()}_Views");
        //        }
        //        return (List<Views>)SessionManager.Get($"{UserLoginGuid}{Utilities.GetAppId()}_Views");
        //    }
        //    set => SessionManager.Set($"{UserLoginGuid}{Utilities.GetAppId()}_Views", value);
        //}

        public static string UserLoginGuid
        {
            get
            {
                if (SessionManager.Get("UserLoginGuid") == null)
                {
                    var cookieVal = CookieHelper.GetFromCookie("basket", "basketid");
                    if (!string.IsNullOrEmpty(cookieVal))
                        SessionManager.Add(cookieVal, "UserLoginGuid");
                    else
                    {
                        var cookieDict = new Dictionary<string, string>();
                        var uniqueId = Utilities.BenzersizAnahtarOlustur();
                        cookieDict.Add("basketid", uniqueId);
                        CookieHelper.StoreInCookie("basket", "", cookieDict, DateTime.Now.AddDays(1), true);
                        SessionManager.Add(uniqueId, "UserLoginGuid");
                    }
                }
                return SessionManager.Get("UserLoginGuid").ToString();
            }
            set => SessionManager.Set("UserLoginGuid", value);
        }
    }
}
