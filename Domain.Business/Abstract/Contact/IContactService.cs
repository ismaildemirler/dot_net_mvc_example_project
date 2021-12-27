using Domain.Entity.Container.Request.Contact;
using Domain.Entity.Container.Response.Contact;

namespace Domain.Business.Abstract.Contact
{
    public interface IContactService
    {
        ResponseContact GetAllContacts();
        void DeleteContact(RequestContact request);
        ResponseContact GetContactByRequest(RequestContact request);
    }
}
