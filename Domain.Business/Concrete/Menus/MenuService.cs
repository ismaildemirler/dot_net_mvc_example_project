using Domain.Business.Abstract.Menus;
using Domain.DataAccess.Abstract.Menus;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Menus;
using Domain.Entity.Container.Response.Customer;
using Domain.Entity.Container.Response.Menus;
using System;
using System.Linq;

namespace Domain.Business.Concrete.Menus
{
    public class MenuService : IMenuService
    {
        private readonly IMenuDAL _menuDAL;

        public MenuService(IMenuDAL menuDAL)
        {
            _menuDAL = menuDAL;
        }

        public ResponseMenu GetAllMenus()
        {
            var response = new ResponseMenu();
            var menus = _menuDAL.GetRepository<Menu>().Queryable().ToList();
            if (menus != null)
            {
                response.Menus = menus;
            }
            return response;
        }

        public ResponseMenu GetAllSubMenus()
        {
            var response = new ResponseMenu();
            var submenus = _menuDAL.GetRepository<SubMenu>().Queryable().ToList();
            if (submenus != null)
            {
                response.SubMenus = submenus;
            }
            return response;
        }
    }
}
