using System;

namespace Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging
{
    [Serializable]
    public class RequestErrorLog
    {
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int? UserId { get; set; }
        public string Ip { get; set; }
        public string ServerName { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
