using Domain.Entity.Concrete;
using Domain.Entity.Enum;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.WebHelpers.Infrastructre;
using System.Linq;
using System.Text;
using System.Web;

namespace Domain.Web.Infrastructure
{
    public class UserViews
    {
        public static string KullaniciMenuStr
        {
            get
            {
                if (HttpContext.Current.Session[CurrentUser.Identity.Id + "_KullaniciMenuStr"] == null)
                    KullaniciMenuYukle();
                return HttpContext.Current.Session[CurrentUser.Identity.Id + "_KullaniciMenuStr"].ToString();
            }
            set => HttpContext.Current.Session[CurrentUser.Identity.Id + "_KullaniciMenuStr"] = value;
        }

        static void KullaniciMenuYukle()
        {
            var sbMenu = new StringBuilder();
            var menus = CacheManager.GetAllMenus.Where(f => f.UserTypeId == (byte)EnumSystemUserType.Customer).OrderBy(a => a.MenuOrder);
            if (CurrentUser.Identity.UserTypeId == (byte)EnumSystemUserType.Admin)
            {
                menus = CacheManager.GetAllMenus.Where(f => f.UserTypeId == (byte)EnumSystemUserType.Admin).OrderBy(a => a.MenuOrder);
            }

            foreach (var menu in menus)
            {
                sbMenu.Append("<li class=\"m-menu__item  m-menu__item--submenu\" data-menu-submenu-toggle='hover' aria-haspopup='true'>");
                var href = menu.MenuUrl == "#" ? "#" : VirtualPathUtility.ToAbsolute("~/" + menu.MenuUrl);
                sbMenu.Append("<a href=\"" + href + "\" class=\"m-menu__link m-menu__toggle\" ><i class=\"m-menu__link-icon " + (string.IsNullOrEmpty(menu.MenuIcon) ? "m-menu__link-bullet m-menu__link-bullet--dot" : menu.MenuIcon) + "\"> <span></span> </i>" +
                              "<span class=\"m-menu__link-text\">&nbsp;" + menu.MenuTitle + "</span><i class='m-menu__ver-arrow fa fa-chevron-right'></i></a>");
                SubMenu(menu, sbMenu);
                sbMenu.Append("</li>");
            }

            KullaniciMenuStr = sbMenu.ToString();
        }

        static void SubMenu(Menu menu, StringBuilder sb)
        {
            var altMenuleri = CacheManager.GetAllSubMenus.Where(f => f.MenuId == menu.MenuId).OrderBy(p => p.SubMenuOrder);
            if (altMenuleri.Any())
            {
                sb.Append("<div class='m-menu__submenu'>");
                sb.Append("<span class='m-menu__arrow'></span>");
                sb.Append("<ul class='m-menu__subnav'>");

                foreach (var altMenu in altMenuleri)
                {
                    sb.Append("<li class=\"m-menu__item\" data-menu-submenu-toggle='hover' aria-haspopup='true'>");
                    var href = altMenu.SubMenuUrl == "#" ? "#" : VirtualPathUtility.ToAbsolute("~/" + altMenu.SubMenuUrl);
                    sb.Append("<a href=\"" + href + "\" class=\"m-menu__link\" ><i class=\"m-menu__link-icon " + (string.IsNullOrEmpty(altMenu.SubMenuIcon) ? "m-menu__link-bullet m-menu__link-bullet--dot" : altMenu.SubMenuIcon) + "\"></i>" +
                              "<span class=\"m-menu__link-text\">&nbsp;" + altMenu.SubMenuTitle + "</span></a>");
                    sb.Append("</a>");
                    sb.Append(" </li>");
                }
                sb.Append(" </ul>");
            }
        }
    }
}