using Domain.Entity.ViewModels.UserBrandApplication;
using System;

namespace Domain.Entity.Container.Request.Contact
{
    public class RequestContact
    {
        public BrandApplicationDetailViewModel BrandApplicationDetail { get; set; }
        public Guid ContactId { get; set; }
    }
}
