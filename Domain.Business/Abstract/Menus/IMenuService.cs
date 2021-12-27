
using Domain.Entity.Container.Request.Menus;
using Domain.Entity.Container.Response.Menus;

namespace Domain.Business.Abstract.Menus
{
    public interface IMenuService
    {
        ResponseMenu GetAllMenus();
        ResponseMenu GetAllSubMenus();
    }
}
