using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Patent;
using Domain.Entity.Container.Request.Services;
using Domain.Entity.Container.Response.Patent;
using Domain.Entity.Container.Response.Services;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Service
{
    public interface IServicesService
    {
        List<ResponseService> GetAllServices(bool withIcon = false);
        void SaveService(RequestService request);
        ResponseService GetService(RequestService request);
        void DeleteService(RequestService request);
    }
}
