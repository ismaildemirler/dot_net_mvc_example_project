using System;

namespace Domain.Infrastructure.ElasticSearch
{
    public class ErrorLog : ElasticSearchBase
    {
        public string MethodName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetail { get; set; }
        public int UserId { get; set; }
        public string Ip { get; set; }
        public string ServerName { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
