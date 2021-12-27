using Domain.Business.Abstract.Contact;
using Domain.DataAccess.Abstract.Contact;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Contact;
using Domain.Entity.Container.Response.Contact;
using Domain.Infrastructure.Aspects.Postsharp.TransactionAspects;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Linq;

namespace Domain.Business.Concrete.Contact
{
    public class ContactService : IContactService
    {
        private readonly IContactDAL _contactDAL;

        public ContactService(IContactDAL contactDAL)
        {
            _contactDAL = contactDAL;
        }

        public ResponseContact GetAllContacts()
        {
            var contacts = _contactDAL.GetRepository<Entity.Concrete.Contact>().Queryable()
                .Where(f => f.CustomerId == CurrentUser.Identity.UserId).ToList();
            var response = new ResponseContact();

            if (contacts != null)
            {
                response.Contacts = contacts;
            }
            return response;
        }

        public ResponseContact GetContactByRequest(RequestContact request)
        {
            var query = _contactDAL.GetRepository<Entity.Concrete.Contact>().Queryable();
            var response = new ResponseContact();

            if (request.ContactId != null && request.ContactId != Guid.Empty)
                query = query.Where(p => p.ContactId == request.ContactId);

            var contact = query.FirstOrDefault();
            if (contact != null)
            {
                response.Contact = contact;
            }
            return response;
        }

        [TransactionScopeAspect]
        public void DeleteContact(RequestContact request)
        {
            var repBrandApplicationDetail 
                = _contactDAL.GetRepository<BrandApplicationDetail>();

            var brandApplicationDetail = new BrandApplicationDetail
            {
                BrandApplicationDetailId = request.BrandApplicationDetail.BrandApplicationDetailId,
                BrandApplicationDate = request.BrandApplicationDetail.BrandApplicationDate,
                BrandApplicationTypeId = request.BrandApplicationDetail.BrandApplicationTypeId,
                BrandCategoryDescription = request.BrandApplicationDetail.BrandCategoryDescription,
                BrandName = request.BrandApplicationDetail.BrandName,
                ContactId = null,
                CustomerApplicationId = request.BrandApplicationDetail.CustomerApplicationId,
                RegistryNumber = request.BrandApplicationDetail.RegistryNumber,
                SendCoverLetter = request.BrandApplicationDetail.SendCoverLetter,
                SpecialClass = request.BrandApplicationDetail.SpecialClass
            };
            repBrandApplicationDetail.Update(brandApplicationDetail);


            var repContact = _contactDAL.GetRepository<Entity.Concrete.Contact>();
            repContact.Delete(request.ContactId);
        }
    }
}
