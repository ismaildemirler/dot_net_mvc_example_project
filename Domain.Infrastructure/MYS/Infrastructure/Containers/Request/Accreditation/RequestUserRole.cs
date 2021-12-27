using System;

namespace Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Accreditation
{
    public class RequestUserRole
    {
        public Guid SystemUserRoleId { get; set; }
        public int SystemUserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
