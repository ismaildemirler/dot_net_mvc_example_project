using Domain.Infrastructure.ElasticSearch;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.Log
{
    public class ResponseErrorLog
    {
        public ResponseErrorLog()
        {
            Logs = new List<ErrorLog>();
        }

        public List<ErrorLog> Logs { get; set; }
    }
}
