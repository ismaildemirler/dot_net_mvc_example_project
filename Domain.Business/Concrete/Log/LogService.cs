using Domain.Business.Abstract.Log;
using Domain.Entity.Container.Response.Log;
using Domain.Infrastructure.ElasticSearch;
using Nest;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Business.Concrete.Log
{
    public class LogService : ILogService
    {
        private readonly ElasticProvider<ErrorLog> _elasticProvider;

        public LogService()
        {
            _elasticProvider = new ElasticProvider<ErrorLog>();
        }

        public ResponseErrorLog GetLast100ElasticErrorLogList()
        {
            Expression<Func<ErrorLog, object>> expression = p => p.ErrorTime;
            var field = new Field(expression);
            var logList = _elasticProvider.Search(null, 0, 100, new SortDescriptor<ErrorLog>().Descending(field), null).ToList();
            var response = new ResponseErrorLog() { Logs = logList };
            return response;
        }
    }
}
