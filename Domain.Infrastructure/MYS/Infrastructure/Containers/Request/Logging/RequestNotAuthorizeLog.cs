using System;

namespace Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging
{
    [Serializable]
    public class RequestNotAuthorizeLog
    {
        public Guid ViewOrAuthId { get; set; }
        public int? UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Ip { get; set; }
        public string TcKimlikNo { get; set; }
    }
}
