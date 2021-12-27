using Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging;
using System;
using System.Collections.Generic;

namespace Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging
{
    public class RequestTableLog
    {
        public RequestTableLog()
        {
            TableLogDetail = new HashSet<RequestTableLogDetail>();
        }
        public string TableName { get; set; }
        public long PrimaryKeyValue { get; set; }
        public Guid? OperationGuid { get; set; }
        public DateTime TimeStamp { get; set; }
        public string TcKimlikNo { get; set; }
        public int? UserId { get; set; }
        public string Ip { get; set; }
        public string State { get; set; }
        public bool IsProxy { get; set; }
        public ICollection<RequestTableLogDetail> TableLogDetail { get; set; }
    }
}


