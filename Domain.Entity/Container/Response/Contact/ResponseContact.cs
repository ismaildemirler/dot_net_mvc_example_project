using System.Collections.Generic;

namespace Domain.Entity.Container.Response.Contact
{
    public class ResponseContact
    {
        public Concrete.Contact Contact { get; set; }
        public List<Concrete.Contact> Contacts { get; set; }
    }
}
