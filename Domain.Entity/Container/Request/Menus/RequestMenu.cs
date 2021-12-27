using System;

namespace Domain.Entity.Container.Request.Menus
{
    public class RequestMenu
    {
        public byte UserTypeId { get; set; }
        public Guid MenuId { get; set; }
        public Guid SubMenuId { get; set; }
    }
}
