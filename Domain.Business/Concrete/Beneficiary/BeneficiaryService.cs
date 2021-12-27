using Domain.Business.Abstract.Beneficiary;
using Domain.DataAccess.Abstract.Beneficiary;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Beneficiary;
using Domain.Entity.Container.Response.Beneficiary;
using Domain.Entity.Container.Response.Bneficiary;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Linq;

namespace Domain.Business.Concrete.Beneficiary
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryDAL _beneficiaryDAL;

        public BeneficiaryService(IBeneficiaryDAL beneficiaryDAL)
        {
            _beneficiaryDAL = beneficiaryDAL;
        }

        public ResponseBeneficiary GetBeneficiaryByRequest(RequestBeneficiary request)
        {
            var query = _beneficiaryDAL.GetRepository<Entity.Concrete.Beneficiary>().Queryable();
            var response = new ResponseBeneficiary();

            if (request.BrandApplicationDetailId != null && request.BrandApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.BrandApplicationDetailId == request.BrandApplicationDetailId);

            if (request.PatentApplicationDetailId != null && request.PatentApplicationDetailId != Guid.Empty)
                query = query.Where(p => p.PatentApplicationDetailId == request.PatentApplicationDetailId);

            if (request.BeneficiaryId != null && request.BeneficiaryId != Guid.Empty)
                query = query.Where(p => p.BeneficiaryId == request.BeneficiaryId);

            var beneficiary = query.FirstOrDefault();
            if (beneficiary != null)
            {
                response.Beneficiary = beneficiary;
            }
            return response;
        }

        public ResponseBeneficiaryRegistry CreateBeneficiaryRegistry(RequestBeneficiaryRegistry request)
        {
            var repBeneficiaryRegistry = _beneficiaryDAL.GetRepository<Entity.Concrete.Beneficiary>();

            if (request.Beneficiary.BeneficiaryId == null ||
                request.Beneficiary.BeneficiaryId == Guid.Empty)
            {
                request.Beneficiary.BeneficiaryId = Guid.NewGuid();

                if (request.Beneficiary.BrandApplicationDetailId != null &&
                    request.Beneficiary.BrandApplicationDetailId != Guid.Empty)
                {
                    request.Beneficiary.PatentApplicationDetailId = null; 
                }
                else if (request.Beneficiary.PatentApplicationDetailId != null &&
                    request.Beneficiary.PatentApplicationDetailId != Guid.Empty)
                {
                    request.Beneficiary.BrandApplicationDetailId = null;
                }
                repBeneficiaryRegistry.Insert(request.Beneficiary);
            }
            else
            {
                var beneficiary = repBeneficiaryRegistry.Queryable().FirstOrDefault(f => f.BeneficiaryId == request.Beneficiary.BeneficiaryId);
                if (beneficiary != null)
                {
                    beneficiary.FirmName = request.Beneficiary.FirmName;
                    beneficiary.TCNumber = request.Beneficiary.TCNumber;
                    beneficiary.TaxOffice = request.Beneficiary.TaxOffice;
                    beneficiary.TaxNumber = request.Beneficiary.TaxNumber;
                    beneficiary.Address = request.Beneficiary.Address;
                    beneficiary.CityId = request.Beneficiary.CityId;
                    beneficiary.TownId = request.Beneficiary.TownId;
                    beneficiary.PhoneNumber = request.Beneficiary.PhoneNumber;
                    beneficiary.Fax = request.Beneficiary.Fax;
                    beneficiary.Description = request.Beneficiary.Description;
                    beneficiary.PreviousFirmName = request.Beneficiary.PreviousFirmName;
                    beneficiary.PreviousOfficialAddress = request.Beneficiary.PreviousOfficialAddress;
                    beneficiary.PersonName = request.Beneficiary.PersonName;
                    beneficiary.BirthDate = request.Beneficiary.BirthDate;
                    beneficiary.BirthPlace = request.Beneficiary.BirthPlace;
                    beneficiary.ExtraBrandName = request.Beneficiary.ExtraBrandName;
                    beneficiary.ExtraBrandRegistryNumber = request.Beneficiary.ExtraBrandRegistryNumber;

                    repBeneficiaryRegistry.Update(beneficiary);

                    request.Beneficiary = beneficiary;
                }
            }

            return new ResponseBeneficiaryRegistry
            {
                Beneficiary = request.Beneficiary
            };
        }

        [TransactionScopeAspect]
        public ResponseBeneficiaryContactRegistry CreateBeneficiaryContactRegistry(RequestBeneficiaryContactRegistry request)
        {
            var repContact = _beneficiaryDAL.GetRepository<Entity.Concrete.Contact>();

            if (request.Contact.ContactId == null || request.Contact.ContactId == Guid.Empty)
            {
                request.Contact.ContactId = Guid.NewGuid();
                request.Contact.CustomerId = CurrentUser.Identity.UserId;
                request.Contact.CreationDate = DateTime.Now;
                request.Contact.UpdateDate = DateTime.Now;
                repContact.Insert(request.Contact);
            }
            else
            {
                var contact = repContact.Queryable().FirstOrDefault(f => f.ContactId == request.Contact.ContactId);
                contact.CustomerId = CurrentUser.Identity.UserId;
                contact.NameSurname = request.Contact.NameSurname;
                contact.Address = request.Contact.Address;
                contact.CityId = request.Contact.CityId;
                contact.TownId = request.Contact.TownId;
                contact.PhoneNumber = request.Contact.PhoneNumber;
                contact.FaxNumber = request.Contact.FaxNumber;
                contact.District = request.Contact.District;
                contact.PostalCode = request.Contact.PostalCode;
                contact.UpdateDate = DateTime.Now;
                repContact.Update(contact);

                request.Contact = contact;
            }

            var repBrandApplication = _beneficiaryDAL.GetRepository<BrandApplicationDetail>();
            request.BrandApplicationDetail.ContactId = request.Contact.ContactId;
            repBrandApplication.Update(request.BrandApplicationDetail);

            return new ResponseBeneficiaryContactRegistry
            {
                Contact = request.Contact
            };
        }
    }
}
