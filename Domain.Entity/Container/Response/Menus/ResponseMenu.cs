using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.Menus
{
    public class ResponseMenu
    {
        public List<Menu> Menus { get; set; }
        public List<SubMenu> SubMenus { get; set; }
    }
}
