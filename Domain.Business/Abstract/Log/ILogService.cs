using Domain.Entity.Container.Response.Log;

namespace Domain.Business.Abstract.Log
{
    public interface ILogService
    {
        ResponseErrorLog GetLast100ElasticErrorLogList();
    }
}
