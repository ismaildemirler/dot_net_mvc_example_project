using Domain.Business.Abstract.Attachment;
using Domain.Business.Abstract.IndustrialDesign;
using Domain.DataAccess.Abstract.IndustrialDesign;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.IndustrialDesign;
using Domain.Entity.Container.Response.IndustrialDesign;
using Domain.Entity.Enum;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.IndustrialDesign
{
    public class IndustrialDesignService : IIndustrialDesignService
    {
        private readonly IIndustrialDesignDAL _industrialDesignDAL;
        private readonly IAttachmentService _attachmentService;

        public IndustrialDesignService(
            IAttachmentService attachmentService,
            IIndustrialDesignDAL industrialDesignDAL)
        {
            _attachmentService = attachmentService;
            _industrialDesignDAL = industrialDesignDAL;
        }

        #region IndustrialDesignApplication

        public List<IndustrialDesignApplicationComplexType> GetAllApplications(string userId, int number = 10)
        {
            return _industrialDesignDAL.GetListSearchedIndustrialDesignApplicationCT(userId).Take(number).ToList();
        }

        [TransactionScopeAspect]
        public ResponseIndustrialDesignApplication CreateIndustrialDesignApplication(
            RequestIndustrialDesignApplication request)
        {
            var repIndustrialDesignApplication
                = _industrialDesignDAL.GetRepository<IndustrialDesignApplicationDetail>();

            if (request.IndustrialDesignApplicationDetail.IndustrialDesignApplicationDetailId == null
                || request.IndustrialDesignApplicationDetail.IndustrialDesignApplicationDetailId == Guid.Empty)
            {
                var currentUser = CurrentUser.Identity;

                var customerApplication = new CustomerApplication
                {
                    ApplyDate = DateTime.Now,
                    ApplicationTypeId = (int)EnumApplicationType.Endustriyel_Tasarim_Basvurusu,
                    CustomerId = currentUser.UserId == 0 ? (int)EnumAnonymousUser.Anonim : currentUser.UserId,
                    CustomerName = currentUser.NameSurname ?? "",
                    Email = currentUser.EMail,
                    StateId = 0,
                    CustomerApplicationId = Guid.NewGuid()
                };

                var repCustomerApplication = _industrialDesignDAL.GetRepository<CustomerApplication>();
                repCustomerApplication.Insert(customerApplication);
                request.CustomerApplication = customerApplication;

                request.IndustrialDesignApplicationDetail.IndustrialDesignApplicationDetailId = Guid.NewGuid();
                request.IndustrialDesignApplicationDetail.DesignApplicationDate = DateTime.Now;
                request.IndustrialDesignApplicationDetail.CustomerApplicationId
                    = customerApplication.CustomerApplicationId;
                repIndustrialDesignApplication.Insert(request.IndustrialDesignApplicationDetail);

                var attachment = new Entity.Concrete.Attachment
                {
                    AttachmentId = request.Attachment.AttachmentId,
                    AttachmentData = request.Attachment.AttachmentData,
                    AttachmentFileName = request.Attachment.AttachmentFileName,
                    AttachmentTypeName = request.Attachment.AttachmentTypeName,
                    FormId = request.Attachment.FormId,
                    RelatedEntity = request.Attachment.RelatedEntity,
                    RelatedEntityId = request.IndustrialDesignApplicationDetail.IndustrialDesignApplicationDetailId
                };

                var repAttachment = _industrialDesignDAL.GetRepository<Entity.Concrete.Attachment>();
                repAttachment.Update(attachment);
            }
            else
            {
                var industrialDesignApplicationDetail = repIndustrialDesignApplication.Queryable()
                    .FirstOrDefault(f => f.IndustrialDesignApplicationDetailId
                    == request.IndustrialDesignApplicationDetail.IndustrialDesignApplicationDetailId);
                if (industrialDesignApplicationDetail != null)
                {
                    var repCustomerApplication = _industrialDesignDAL.GetRepository<CustomerApplication>();
                    var customerApplication = repCustomerApplication.Queryable()
                        .FirstOrDefault(f => f.CustomerApplicationId
                        == industrialDesignApplicationDetail.CustomerApplicationId);

                    if (customerApplication != null)
                    {
                        var currentUser = CurrentUser.Identity;
                        customerApplication.ApplicationTypeId = request.CustomerApplication.ApplicationTypeId;
                        customerApplication.CustomerId = currentUser.UserId == 0 ? (int)EnumAnonymousUser.Anonim : currentUser.UserId;
                        customerApplication.CustomerName = currentUser.NameSurname ?? "";
                        customerApplication.Email = currentUser.EMail;
                        repCustomerApplication.Update(customerApplication);
                        request.CustomerApplication = customerApplication;
                    }

                    industrialDesignApplicationDetail.Title = request.IndustrialDesignApplicationDetail.Title;

                    repIndustrialDesignApplication.Update(industrialDesignApplicationDetail);
                    request.IndustrialDesignApplicationDetail = industrialDesignApplicationDetail;
                }
            }

            return new ResponseIndustrialDesignApplication
            {
                CustomerApplication = request.CustomerApplication,
                IndustrialDesignApplicationDetail = request.IndustrialDesignApplicationDetail
            };
        }

        public ResponseIndustrialDesignApplicationDetail GetIndustrialDesignApplicationDetailByRequest(
            RequestIndustrialDesignApplicationDetail request)
        {
            var query = _industrialDesignDAL.GetRepository<IndustrialDesignApplicationDetail>().Queryable();
            var response = new ResponseIndustrialDesignApplicationDetail();

            if (request.IndustrialDesignApplicationDetailId != null
                && request.IndustrialDesignApplicationDetailId != Guid.Empty)
            {
                query = query.Where(p => p.IndustrialDesignApplicationDetailId
                    == request.IndustrialDesignApplicationDetailId);
            }

            if (request.CustomerApplicationId != null && request.CustomerApplicationId != Guid.Empty)
                query = query.Where(p => p.CustomerApplicationId == request.CustomerApplicationId);

            var industrialDesignApplicationDetail = query.FirstOrDefault();
            if (industrialDesignApplicationDetail != null)
            {
                response.IndustrialDesignApplicationDetail = industrialDesignApplicationDetail;
            }
            return response;
        }

        public PagedList<IndustrialDesignApplicationComplexType> GetPagedListSearchedIndustrialDesignApplicationCT(
            DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters)
        {
            return _industrialDesignDAL.GetPagedListSearchedIndustrialDesignApplicationCT(
                dtParameterModel, searchFilters);
        }

        [TransactionScopeAspect]
        public void DeleteIndustrialDesign(RequestIndustrialDesignApplicationDetail request)
        {
            var customerApplicationId = new Guid();

            var repIndustrialDesignApplicationDetail = _industrialDesignDAL.GetRepository<IndustrialDesignApplicationDetail>();
            var industrialDesignApplicationDetail = repIndustrialDesignApplicationDetail.Queryable()
                .FirstOrDefault(f => f.IndustrialDesignApplicationDetailId
                    == request.IndustrialDesignApplicationDetailId);

            var attachments = _attachmentService.GetAttachmentsWithoutData(
                    new AttachmentComplexType
                    {
                        RelatedEntityId = industrialDesignApplicationDetail.IndustrialDesignApplicationDetailId,
                        FormType = EnumFormType.Endustriyel_Tasarim_Coklu_Fotograf_Dosyasi
                    });

            var repAttachment = _industrialDesignDAL.GetRepository<Entity.Concrete.Attachment>();

            foreach (var attachment in attachments)
            {
                repAttachment.Delete(attachment.AttachmentId);
            }

            if (industrialDesignApplicationDetail != null)
            {
                customerApplicationId = industrialDesignApplicationDetail.CustomerApplicationId;
                repIndustrialDesignApplicationDetail.Delete(industrialDesignApplicationDetail);
            }

            var repCustomerApplication = _industrialDesignDAL.GetRepository<CustomerApplication>();
            if (customerApplicationId != Guid.Empty)
            {
                var customerApplication = repCustomerApplication.Queryable()
                                .FirstOrDefault(f => f.CustomerApplicationId == customerApplicationId);
                if (customerApplication != null)
                {
                    repCustomerApplication.Delete(customerApplication);
                }
            }
        }
        #endregion
    }
}
