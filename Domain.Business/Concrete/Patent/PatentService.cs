using Domain.Business.Abstract.Patent;
using Domain.DataAccess.Abstract.Patent;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Patent;
using Domain.Entity.Container.Response.Patent;
using Domain.Entity.Enum;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Patent
{
    public class PatentService : IPatentService
    {
        private readonly IPatentDAL _patentDAL;

        public PatentService(IPatentDAL patentDAL)
        {
            _patentDAL = patentDAL;
        }

        #region PatentApplication
        public ResponsePatentApplicationDetail GetPatentApplicationDetailByRequest(RequestPatentApplicationDetail request)
        {
            var query = _patentDAL.GetRepository<PatentApplicationDetail>().Queryable();
            var response = new ResponsePatentApplicationDetail();

            if (request.PatentApplicationDetailId != null && request.PatentApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.PatentApplicationDetailId == request.PatentApplicationDetailId);

            if (request.CustomerApplicationId != null && request.CustomerApplicationId != Guid.Empty)
                query = query.Where(p => p.CustomerApplicationId == request.CustomerApplicationId);

            if (!string.IsNullOrWhiteSpace(request.Title))
                query = query.Where(p => p.Title == request.Title);

            var patentApplicationDetail = query.FirstOrDefault();
            if (patentApplicationDetail != null)
            {
                response.PatentApplicationDetail = patentApplicationDetail;
            }
            return response;
        }

        public List<PatentApplicationComplexType> GetAllApplications(string userId, int number = 10)
        {
            return _patentDAL.GetListSearchedPatentApplicationCT(userId);
        }

        public PagedList<PatentApplicationComplexType> GetPagedListSearchedPatentApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters)
        {
            return _patentDAL.GetPagedListSearchedPatentApplicationCT(dtParameterModel, searchFilters);
        }

        [TransactionScopeAspect]
        public ResponsePatentApplication CreatePatentApplication(RequestPatentApplication request)
        {
            var repPatentApplication = _patentDAL.GetRepository<PatentApplicationDetail>();

            if (request.PatentApplicationDetail.PatentApplicationDetailId == null
                || request.PatentApplicationDetail.PatentApplicationDetailId == Guid.Empty)
            {
                var currentUser = CurrentUser.Identity;

                var customerApplication = new CustomerApplication
                {
                    ApplyDate = DateTime.Now,
                    ApplicationTypeId = (int)EnumApplicationType.Patent_Basvurusu,
                    CustomerId = currentUser.UserId == 0 ? (int)EnumAnonymousUser.Anonim : currentUser.UserId,
                    CustomerName 
                        = !string.IsNullOrWhiteSpace(request.CustomerApplication.CustomerName) 
                            ? request.CustomerApplication.CustomerName 
                            : (!string.IsNullOrWhiteSpace(currentUser.NameSurname) 
                                ? currentUser.NameSurname
                                : ""),
                    Email = !string.IsNullOrWhiteSpace(request.CustomerApplication.Email)
                            ? request.CustomerApplication.Email
                            : (!string.IsNullOrWhiteSpace(currentUser.EMail)
                                ? currentUser.EMail
                                : ""),
                    AnonymousApplicationCode 
                        = request.CustomerApplication.AnonymousApplicationCode ?? "",
                    PhoneNumber
                        = !string.IsNullOrWhiteSpace(request.CustomerApplication.PhoneNumber)
                            ? request.CustomerApplication.PhoneNumber
                            : (!string.IsNullOrWhiteSpace(currentUser.PhoneNumber)
                                ? currentUser.PhoneNumber
                                : ""),
                    StateId = 0,
                    CustomerApplicationId = Guid.NewGuid()
                };

                var repCustomerApplication = _patentDAL.GetRepository<CustomerApplication>();
                repCustomerApplication.Insert(customerApplication);
                request.CustomerApplication = customerApplication;


                request.PatentApplicationDetail.PatentApplicationDetailId = Guid.NewGuid();
                request.PatentApplicationDetail.CustomerApplicationId = customerApplication.CustomerApplicationId;
                repPatentApplication.Insert(request.PatentApplicationDetail);
            }
            else
            {
                var patentApplicationDetail = repPatentApplication.Queryable()
                    .FirstOrDefault(f => f.PatentApplicationDetailId == request.PatentApplicationDetail.PatentApplicationDetailId);
                if (patentApplicationDetail != null)
                {
                    var repCustomerApplication = _patentDAL.GetRepository<CustomerApplication>();
                    var customerApplication = repCustomerApplication.Queryable()
                        .FirstOrDefault(f => f.CustomerApplicationId == patentApplicationDetail.CustomerApplicationId);

                    if (customerApplication != null)
                    {
                        var currentUser = CurrentUser.Identity;
                        customerApplication.ApplicationTypeId 
                            = request.CustomerApplication.ApplicationTypeId;
                        customerApplication.CustomerId
                            = request.CustomerApplication.CustomerId;
                        customerApplication.CustomerName 
                            = request.CustomerApplication.CustomerName;
                        customerApplication.Email 
                            = request.CustomerApplication.Email;
                        customerApplication.AnonymousApplicationCode 
                            = request.CustomerApplication.AnonymousApplicationCode;
                        customerApplication.PhoneNumber = request.CustomerApplication.PhoneNumber;
                        repCustomerApplication.Update(customerApplication);
                        request.CustomerApplication = customerApplication;
                    }

                    patentApplicationDetail.PlannedFeatures = request.PatentApplicationDetail.PlannedFeatures;
                    patentApplicationDetail.Mail = request.PatentApplicationDetail.Mail;
                    patentApplicationDetail.PresentFeatures = request.PatentApplicationDetail.PresentFeatures;
                    patentApplicationDetail.Title = request.PatentApplicationDetail.Title;
                    patentApplicationDetail.Summary = request.PatentApplicationDetail.Summary;

                    repPatentApplication.Update(patentApplicationDetail);
                    request.PatentApplicationDetail = patentApplicationDetail;
                }
            }

            return new ResponsePatentApplication
            {
                CustomerApplication = request.CustomerApplication,
                PatentApplicationDetail = request.PatentApplicationDetail
            };
        }

        [TransactionScopeAspect]
        public void DeletePatent(RequestPatentApplicationDetail request)
        {
            var repBeneficiary = _patentDAL.GetRepository<Entity.Concrete.Beneficiary>();
            var beneficiary = repBeneficiary.Queryable()
                .FirstOrDefault(f => f.PatentApplicationDetailId == request.PatentApplicationDetailId);
            if (beneficiary != null)
            {
                repBeneficiary.Delete(beneficiary);
            }

            var repAttachment = _patentDAL.GetRepository<Entity.Concrete.Attachment>();
            var attachments = repAttachment.Queryable()
                .Where(f => f.RelatedEntityId == request.PatentApplicationDetailId);
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    repAttachment.Delete(attachment);
                }
            }

            var customerApplicationId = new Guid();

            var repPatentApplicationDetail = _patentDAL.GetRepository<PatentApplicationDetail>();
            var patentApplicationDetail = repPatentApplicationDetail.Queryable()
                .FirstOrDefault(f => f.PatentApplicationDetailId == request.PatentApplicationDetailId);
            if (patentApplicationDetail != null)
            {
                customerApplicationId = patentApplicationDetail.CustomerApplicationId;
                repPatentApplicationDetail.Delete(patentApplicationDetail);
            }

            var repCustomerApplication = _patentDAL.GetRepository<CustomerApplication>();
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
