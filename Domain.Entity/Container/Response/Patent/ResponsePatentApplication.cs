using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Response.Patent
{
    public class ResponsePatentApplication
    {
        public CustomerApplication CustomerApplication { get; set; }
        public PatentApplicationDetail PatentApplicationDetail { get; set; }
    }
}
