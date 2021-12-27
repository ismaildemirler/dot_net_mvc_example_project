using Domain.Entity.Concrete;
using System;

namespace Domain.Entity.Container.Request.Patent
{
    public class RequestPatentApplication
    {
        public CustomerApplication CustomerApplication { get; set; }
        public PatentApplicationDetail PatentApplicationDetail { get; set; }
    }
}
