using Domain.Entity.Concrete;
using Domain.Entity.Enum;

namespace Domain.Entity.Container.Request.SystemUsers
{
    public class RequestSystemUser
    {
        public int SystemUserId { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public EnumSystemUserType UserType { get; set; }
        public SystemUser SystemUserInfo { get; set; }
    }
}
