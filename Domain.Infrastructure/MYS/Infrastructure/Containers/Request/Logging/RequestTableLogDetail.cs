using System;

namespace Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging
{

    [Serializable]
    public class RequestTableLogDetail
    {
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

    }
}
