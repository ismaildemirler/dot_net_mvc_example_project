using Domain.Business.Abstract.Brand;
using Domain.DataAccess.Abstract.Brand;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Brand;
using Domain.Entity.Container.Request.Customer;
using Domain.Entity.Container.Response.Brand;
using Domain.Entity.Container.Response.Customer;
using Domain.Entity.Enum;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Domain.Business.Concrete.Brand
{
    public class BrandService : IBrandService
    {
        private readonly IBrandDAL _brandDAL;

        public BrandService(IBrandDAL brandDAL)
        {
            _brandDAL = brandDAL;
        }

        #region BrandApplication
        public ResponseBrandApplicationDetail GetCustomerApplicationBrandSubClassesByRequest(RequestBrandApplicationDetail request)
        {
            var query = _brandDAL.GetRepository<CustomerApplicationBrandClasses>().Queryable();
            var response = new ResponseBrandApplicationDetail();

            if (request.BrandApplicationDetailId != null && request.BrandApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.BrandApplicationDetailId == request.BrandApplicationDetailId);

            var customerApplicationBrandClasses = query.ToList();
            if (customerApplicationBrandClasses != null)
            {
                response.CustomerApplicationBrandClasses = customerApplicationBrandClasses;
            }
            return response;
        }

        public ResponseBrandApplicationDetail GetBrandApplicationDetailByRequest(RequestBrandApplicationDetail request)
        {
            var query = _brandDAL.GetRepository<BrandApplicationDetail>().Queryable();
            var response = new ResponseBrandApplicationDetail();

            if (request.BrandApplicationDetailId != null && request.BrandApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.BrandApplicationDetailId == request.BrandApplicationDetailId);

            if (request.CustomerApplicationId != null && request.CustomerApplicationId != Guid.Empty)
                query = query.Where(p => p.CustomerApplicationId == request.CustomerApplicationId);

            if (!string.IsNullOrWhiteSpace(request.BrandName))
                query = query.Where(p => p.BrandName == request.BrandName);

            var brandApplicationDetail = query.FirstOrDefault();
            if (brandApplicationDetail != null)
            {
                response.BrandApplicationDetail = brandApplicationDetail;
            }
            return response;
        }

        public PagedList<BrandApplicationComplexType> GetPagedListSearchedBrandApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters)
        {
            return _brandDAL.GetPagedListSearchedBrandApplicationCT(dtParameterModel, searchFilters);
        }

        public List<BrandApplicationComplexType> GetAllApplications(
            string userId, bool isBrand, int number = 10)
        {
            return _brandDAL.GetListSearchedBrandApplicationCT(userId, isBrand).Take(number).ToList();
        }

        [TransactionScopeAspect]
        public ResponseBrandApplication CreateBrandApplication(RequestBrandApplication request)
        {
            var repBrandApplication = _brandDAL.GetRepository<BrandApplicationDetail>();

            if (request.BrandApplicationDetail.BrandApplicationDetailId == null
                || request.BrandApplicationDetail.BrandApplicationDetailId == Guid.Empty)
            {
                var currentUser = CurrentUser.Identity;
                var customerApplication = new CustomerApplication
                {
                    ApplyDate = DateTime.Now,
                    ApplicationTypeId = request.CustomerApplication.ApplicationTypeId,
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

                var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
                repCustomerApplication.Insert(customerApplication);
                request.CustomerApplication = customerApplication;

                if (!request.BrandApplicationDetail.SendCoverLetter)
                {
                    request.BrandApplicationDetail.SendCoverLetter = false;
                }

                if (request.BrandApplicationDetail.BrandApplicationDate == DateTime.MinValue)
                {
                    request.BrandApplicationDetail.BrandApplicationDate = null;
                }

                request.BrandApplicationDetail.BrandApplicationDetailId = request.BrandApplicationDetailIdForInsert;
                request.BrandApplicationDetail.CustomerApplicationId = customerApplication.CustomerApplicationId;
                repBrandApplication.Insert(request.BrandApplicationDetail);
            }
            else
            {
                var brandApplicationDetail = repBrandApplication.Queryable()
                    .FirstOrDefault(f => f.BrandApplicationDetailId == request.BrandApplicationDetail.BrandApplicationDetailId);
                if (brandApplicationDetail != null)
                {
                    var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
                    var customerApplication = repCustomerApplication.Queryable()
                        .FirstOrDefault(f => f.CustomerApplicationId == brandApplicationDetail.CustomerApplicationId);

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

                    brandApplicationDetail.BrandCategoryDescription = request.BrandApplicationDetail.BrandCategoryDescription;
                    brandApplicationDetail.BrandApplicationTypeId = request.BrandApplicationDetail.BrandApplicationTypeId;
                    brandApplicationDetail.BrandName = request.BrandApplicationDetail.BrandName;
                    brandApplicationDetail.SpecialClass = request.BrandApplicationDetail.SpecialClass;
                    brandApplicationDetail.SendCoverLetter = request.BrandApplicationDetail.SendCoverLetter;
                    brandApplicationDetail.RegistryNumber = request.BrandApplicationDetail.RegistryNumber;
                    brandApplicationDetail.BrandApplicationDate = request.BrandApplicationDetail.BrandApplicationDate;

                    repBrandApplication.Update(brandApplicationDetail);
                    request.BrandApplicationDetail = brandApplicationDetail;
                }
            }

            if (request.CustomerApplicationBrandClasses != null)
            {
                var repCustomerAppBrandClasses = _brandDAL.GetRepository<CustomerApplicationBrandClasses>();

                var brandClasses = repCustomerAppBrandClasses.Queryable()
                    .Where(f => f.BrandApplicationDetailId == request.BrandApplicationDetail.BrandApplicationDetailId);

                foreach (var brandClass in brandClasses)
                {
                    repCustomerAppBrandClasses.Delete(brandClass);
                }

                foreach (var customerAppBrandClass in request.CustomerApplicationBrandClasses)
                {
                    repCustomerAppBrandClasses.Insert(customerAppBrandClass);
                }
            }

            return new ResponseBrandApplication
            {
                CustomerApplication = request.CustomerApplication,
                BrandApplicationDetail = request.BrandApplicationDetail
            };
        }

        [TransactionScopeAspect]
        public void DeleteBrand(RequestBrandApplicationDetail request)
        {
            var repCustomerAppBrandClasses = _brandDAL.GetRepository<CustomerApplicationBrandClasses>();
            var brandClasses = repCustomerAppBrandClasses.Queryable()
                .Where(f => f.BrandApplicationDetailId == request.BrandApplicationDetailId);
            foreach (var brandClass in brandClasses)
            {
                repCustomerAppBrandClasses.Delete(brandClass);
            }

            var repAttachment = _brandDAL.GetRepository<Entity.Concrete.Attachment>();
            var attachments = repAttachment.Queryable()
                .Where(f => f.RelatedEntityId == request.BrandApplicationDetailId);
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    repAttachment.Delete(attachment);
                }
            }

            var repBeneficiary = _brandDAL.GetRepository<Entity.Concrete.Beneficiary>();
            var beneficiary = repBeneficiary.Queryable()
                .FirstOrDefault(f => f.BrandApplicationDetailId == request.BrandApplicationDetailId);
            if (beneficiary != null)
            {
                repBeneficiary.Delete(beneficiary);
            }

            var customerApplicationId = new Guid();
            var contactID = new Guid();

            var repBrandApplicationDetail = _brandDAL.GetRepository<BrandApplicationDetail>();
            var brandApplicationDetail = repBrandApplicationDetail.Queryable()
                .FirstOrDefault(f => f.BrandApplicationDetailId == request.BrandApplicationDetailId);
            if (brandApplicationDetail != null)
            {
                if (brandApplicationDetail.ContactId != null)
                {
                    contactID = brandApplicationDetail.ContactId.Value;
                }
                customerApplicationId = brandApplicationDetail.CustomerApplicationId;
                repBrandApplicationDetail.Delete(brandApplicationDetail);
            }

            var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
            if (customerApplicationId != Guid.Empty)
            {
                var customerApplication = repCustomerApplication.Queryable()
                                .FirstOrDefault(f => f.CustomerApplicationId == customerApplicationId);
                if (customerApplication != null)
                {
                    repCustomerApplication.Delete(customerApplication);
                }
            }

            var repContact = _brandDAL.GetRepository<Entity.Concrete.Contact>();
            if (contactID != Guid.Empty)
            {
                var contact = repContact.Queryable()
                                .FirstOrDefault(f => f.ContactId == contactID);
                if (contact != null)
                {
                    repContact.Delete(contact);
                }
            }
        }
        #endregion

        #region BrandWatchApplication
        public List<BrandForWatchingApplicationComplexType> GetAllBrandWatchApplications(
            string userId, int number = 10)
        {
            return _brandDAL.GetListSearchedBrandForWatchingApplicationCT(userId).Take(number).ToList();
        }

        public ResponseBrandWatchingApplicationDetail GetBrandWatchApplicationDetailByRequest(
            RequestBrandWatchingApplicationDetail request)
        {
            var query = _brandDAL.GetRepository<BrandWatchingApplicationDetail>().Queryable();
            var response = new ResponseBrandWatchingApplicationDetail();

            if (request.BrandWatchingApplicationDetailId != null || request.BrandWatchingApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.BrandWatchingApplicationDetailId == request.BrandWatchingApplicationDetailId);

            var brandWatchApplicationDetail = query.FirstOrDefault();
            if (brandWatchApplicationDetail != null)
            {
                response.BrandWatchingApplicationDetail = brandWatchApplicationDetail;
            }
            return response;
        }

        public List<BrandForWatching> GetAllBrandWatchApplications(int number = 10)
        {
            var query = _brandDAL.GetRepository<BrandForWatching>().Queryable();
            return query.Take(number).ToList();
        }

        public ResponseBrandForWatching GetBrandForWatchingByRequest(RequestBrandForWatching request)
        {
            var query = _brandDAL.GetRepository<BrandForWatching>().Queryable();
            var response = new ResponseBrandForWatching();

            if (request.BrandWatchApplicationDetailId != null && request.BrandWatchApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.BrandWatchingApplicationDetailId == request.BrandWatchApplicationDetailId);

            if (request.BrandForWatchingId != null && request.BrandForWatchingId != Guid.Empty)
                query = query.Where(p => p.BrandForWatchingId == request.BrandForWatchingId);

            var brandForWatchings = query.ToList();
            if (brandForWatchings != null)
            {
                response.BrandForWatchings = brandForWatchings;
            }
            return response;
        }

        [TransactionScopeAspect]
        public ResponseBrandWatchingApplication CreateBrandWatchApplication(RequestBrandWatchingApplication request)
        {
            var repBrandWatchingApplication = _brandDAL.GetRepository<BrandWatchingApplicationDetail>();

            if (request.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId == null
                || request.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId == Guid.Empty)
            {
                var currentUser = CurrentUser.Identity;
                var customerApplication = new CustomerApplication
                {
                    ApplyDate = DateTime.Now,
                    ApplicationTypeId = (byte)EnumApplicationType.Marka_Izleme_Basvurusu,
                    CustomerId = currentUser.UserId,
                    CustomerName = currentUser.NameSurname,
                    Email = currentUser.EMail,
                    StateId = 0,
                    CustomerApplicationId = Guid.NewGuid()
                };

                var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
                repCustomerApplication.Insert(customerApplication);
                request.CustomerApplication = customerApplication;

                request.BrandWatchingApplicationDetail.CustomerApplicationId = customerApplication.CustomerApplicationId;
                request.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId = Guid.NewGuid();
                repBrandWatchingApplication.Insert(request.BrandWatchingApplicationDetail);
            }
            else
            {
                var brandWatchApplicationDetail = repBrandWatchingApplication.Queryable()
                    .FirstOrDefault(f => f.BrandWatchingApplicationDetailId == request.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId);
                if (brandWatchApplicationDetail != null)
                {
                    var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
                    var customerApplication = repCustomerApplication.Queryable()
                        .FirstOrDefault(f => f.CustomerApplicationId == brandWatchApplicationDetail.CustomerApplicationId);

                    if (customerApplication != null)
                    {
                        var currentUser = CurrentUser.Identity;
                        customerApplication.ApplicationTypeId = (byte)EnumApplicationType.Marka_Izleme_Basvurusu;
                        customerApplication.CustomerId = currentUser.UserId;
                        customerApplication.CustomerName = currentUser.NameSurname;
                        customerApplication.Email = currentUser.EMail;
                        repCustomerApplication.Update(customerApplication);
                        request.CustomerApplication = customerApplication;
                    }

                    brandWatchApplicationDetail.IdentityNumber = request.BrandWatchingApplicationDetail.IdentityNumber;
                    brandWatchApplicationDetail.Address = request.BrandWatchingApplicationDetail.Address;
                    brandWatchApplicationDetail.CityId = request.BrandWatchingApplicationDetail.CityId;
                    brandWatchApplicationDetail.TownId = request.BrandWatchingApplicationDetail.TownId;
                    brandWatchApplicationDetail.Phone = request.BrandWatchingApplicationDetail.Phone;
                    brandWatchApplicationDetail.Fax = request.BrandWatchingApplicationDetail.Fax;

                    repBrandWatchingApplication.Update(brandWatchApplicationDetail);
                    request.BrandWatchingApplicationDetail = brandWatchApplicationDetail;
                }
            }

            return new ResponseBrandWatchingApplication
            {
                CustomerApplication = request.CustomerApplication,
                BrandWatchingApplicationDetail = request.BrandWatchingApplicationDetail
            };
        }

        public ResponseBrandForWatching CreateBrandForWatchingRegistry(RequestBrandForWatchingRegistration request)
        {
            var repBrandForWatchingRegistry = _brandDAL.GetRepository<BrandForWatching>();

            if (request.BrandForWatching.BrandForWatchingId == null ||
                request.BrandForWatching.BrandForWatchingId == Guid.Empty)
            {
                request.BrandForWatching.BrandForWatchingId = Guid.NewGuid();
                repBrandForWatchingRegistry.Insert(request.BrandForWatching);
            }
            else
            {
                var brandForWatching = repBrandForWatchingRegistry.Queryable()
                    .FirstOrDefault(f => f.BrandForWatchingId == request.BrandForWatching.BrandForWatchingId);
                if (brandForWatching != null)
                {
                    brandForWatching.BrandName = request.BrandForWatching.BrandName;
                    brandForWatching.FirmName = request.BrandForWatching.FirmName;
                    brandForWatching.ClassesToWatch = request.BrandForWatching.ClassesToWatch;
                    brandForWatching.RegistryNumber = request.BrandForWatching.RegistryNumber;

                    repBrandForWatchingRegistry.Update(brandForWatching);

                    request.BrandForWatching = brandForWatching;
                }
            }

            return new ResponseBrandForWatching
            {
                BrandForWatching = request.BrandForWatching
            };
        }

        public void DeleteBrandForWatching(RequestBrandForWatching request)
        {
            var repBrandForWatching = _brandDAL.GetRepository<BrandForWatching>();
            repBrandForWatching.Delete(request.BrandForWatchingId);
        }

        public PagedList<BrandForWatchingApplicationComplexType> GetPagedListSearchedBrandForWatchingApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters)
        {
            return _brandDAL.GetPagedListSearchedBrandForWatchingApplicationCT(dtParameterModel, searchFilters);
        }

        public PagedList<BrandWatchingApplicationDetail> GetNotCompletedPagedListSearchedBrandForWatchingApplicationCT(
            DtParameterModel dtParameterModel,
            RequestBrandWatchingApplicationDetail request)
        {
            var applications = new List<BrandWatchingApplicationDetail>();
            var repBrandForWatching = _brandDAL.GetRepository<BrandForWatching>();
            var repBrandWatchingApplicationDetail = _brandDAL.GetRepository<BrandWatchingApplicationDetail>();
            foreach (var item in repBrandWatchingApplicationDetail.Queryable().Where(f => f.BrandWatchingApplicationDetailId != null))
            {
                if (!repBrandForWatching.Queryable().Any(f => f.BrandWatchingApplicationDetailId == item.BrandWatchingApplicationDetailId))
                {
                    applications.Add(item);
                }
            }

            var pagedList = new PagedList<BrandWatchingApplicationDetail>
            {
                PageIndex = dtParameterModel.AramaKriteri.Baslangic.Value,
                PageSize = dtParameterModel.AramaKriteri.Uzunluk.Value,
                IndexFrom = 0,
                TotalCount = applications.Count,
                Items = applications,
                TotalPages = (int)Math.Ceiling(applications.Count / (double)dtParameterModel.AramaKriteri.Uzunluk.Value)
            };

            return pagedList;
        }

        [TransactionScopeAspect]
        public void DeleteBrandForWatchingApplication(RequestBrandForWatching request)
        {
            var repWatchings = _brandDAL.GetRepository<BrandForWatching>();
            var watchingBrands = repWatchings.Queryable()
                .Where(f => f.BrandWatchingApplicationDetailId
                    == request.BrandWatchApplicationDetailId);
            if (watchingBrands != null)
            {
                foreach (var watchingBrand in watchingBrands)
                {
                    repWatchings.Delete(watchingBrand);
                }
            }

            var customerApplicationId = new Guid();

            var repBrandWatchingApplicationDetail = _brandDAL.GetRepository<BrandWatchingApplicationDetail>();
            var brandWatchingApplicationDetail = repBrandWatchingApplicationDetail.Queryable()
                .FirstOrDefault(f => f.BrandWatchingApplicationDetailId == request.BrandWatchApplicationDetailId);
            if (brandWatchingApplicationDetail != null)
            {
                customerApplicationId = brandWatchingApplicationDetail.CustomerApplicationId;
                repBrandWatchingApplicationDetail.Delete(brandWatchingApplicationDetail);
            }

            var repCustomerApplication = _brandDAL.GetRepository<CustomerApplication>();
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
