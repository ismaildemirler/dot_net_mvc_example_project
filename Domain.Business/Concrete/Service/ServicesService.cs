using Domain.Business.Abstract.Service;
using Domain.DataAccess.Abstract.Blog;
using Domain.DataAccess.Abstract.Parameter;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Blog;
using Domain.Entity.Container.Request.Services;
using Domain.Entity.Container.Response.Services;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Service
{
    public class ServicesService : IServicesService
    {
        private readonly IParameterDAL _DAL;

        public ServicesService(IParameterDAL DAL)
        {
            _DAL = DAL;
        }

        public List<ResponseService> GetAllServices(bool withIcon = false)
        {
            var query = _DAL.GetRepository<Services>().Queryable().Where(w => w.IsActive == true);
            if(withIcon)
            {
                return query.Select(s => new ResponseService
                {
                    ServiceID = s.ServiceID,
                    Header = s.Header,
                    DescriptionText = s.DescriptionText,
                    Icon = s.Icon
                }).ToList();
            }
            else
            {
                return query.Select(s => new ResponseService
                {
                    ServiceID = s.ServiceID,
                    Header = s.Header
                }).ToList();
            }
        }
        public ResponseService GetService(RequestService request)
        {
            var service = _DAL.GetRepository<Services>().Find(request.ServiceID);
            if (service == null)
                throw new BusinessException("Kayıt bulunamadı!");

            ResponseService response = new ResponseService()
            {
                ServiceID = service.ServiceID,
                ContentText = service.ContentText,
                DescriptionText = service.DescriptionText,
                Header = service.Header,
                Icon = service.Icon,
                ServiceImage = service.ServiceImage
            };

            return response;
        }
        public void SaveService(RequestService request)
        {
            var rep = _DAL.GetRepository<Services>();

            if (request.ServiceID > 0)
            {
                Services entity = rep.Find(request.ServiceID);
                if (entity == null)
                    throw new BusinessException(Messages.KayitBulunamadi);

                entity.Header = request.Header;
                entity.ContentText= request.ContentText;
                entity.DescriptionText = request.DescriptionText;
                
                if (request.IconDeleted)
                    entity.Icon = request.Icon;

                if(request.ImageDeleted)
                    entity.ServiceImage = request.ServiceImage;

                rep.Update(entity);
            }
            else
            {
                Services service = new Services()
                {
                    ContentText = request.ContentText,
                    DescriptionText = request.DescriptionText,
                    Header = request.Header,
                    Icon = request.Icon,
                    IsActive = true,
                    ServiceImage = request.ServiceImage
                };

                rep.Insert(service);
            }
        }
        public void DeleteService(RequestService request)
        {
            _DAL.GetRepository<Services>().Delete(request.ServiceID);
        }
    }
}
