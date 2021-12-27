using Domain.Business.Abstract.Management;
using Domain.DataAccess.Abstract.Management;
using Domain.Entity.ComplexType;
using Domain.Entity.Container.Request.Manage;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System;
using System.Linq;

namespace Domain.Business.Concrete.Management
{
    public class ManagementService : IManagementService
    {
        private readonly IManagementDAL _managementDAL;
        private readonly IRepository<Entity.Concrete.Management> _repManagement;

        public ManagementService(IManagementDAL managementDAL)
        {
            _managementDAL = managementDAL;
            _repManagement = _managementDAL.GetRepository<Entity.Concrete.Management>();
        }

        public string GetAboutPageContent()
        {
            return _repManagement.Queryable().Select(s=>s.AboutPageContent).FirstOrDefault();
        }

        public ContactPageComplexType GetContactPageInformation()
        {
            return _repManagement.Queryable().Select(s=> new ContactPageComplexType() {
                FirmAddress = s.FirmAddress,
                FirmEMail = s.FirmEMail,
                FirmPhoneNumber = s.FirmPhoneNumber
            }).FirstOrDefault();
        }

        public void SaveAboutPageContent(RequestManagement request)
        {
            var entity = _repManagement.Queryable().FirstOrDefault();
            if (entity == null)
            {
                entity = new Entity.Concrete.Management() { 
                    ManagementId = Guid.NewGuid(),
                    AboutPageContent = request.AboutPageContent   
                };
                _repManagement.Insert(entity);
            }
            else
            {
                entity.AboutPageContent = request.AboutPageContent;
                _repManagement.Update(entity);
            }
        }

        public void SaveContactPageInformation(RequestManagement request)
        {
            var entity = _repManagement.Queryable().FirstOrDefault();
            if (entity == null)
            {
                entity = new Entity.Concrete.Management()
                {
                    ManagementId = Guid.NewGuid(),
                    FirmAddress = request.FirmAddress,
                    FirmEMail = request.FirmEMail,
                    FirmPhoneNumber = request.FirmPhoneNumber
                };
                _repManagement.Insert(entity);
            }
            else
            {
                entity.FirmAddress = request.FirmAddress;
                entity.FirmEMail = request.FirmEMail;
                entity.FirmPhoneNumber = request.FirmPhoneNumber;
                _repManagement.Update(entity);
            }
        }
    }
}
