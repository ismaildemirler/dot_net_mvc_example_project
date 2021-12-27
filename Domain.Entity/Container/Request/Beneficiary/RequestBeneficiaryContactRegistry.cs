using Domain.Entity.Concrete;
using System;

namespace Domain.Entity.Container.Request.Beneficiary
{
    public class RequestBeneficiaryContactRegistry
    {
        public BrandApplicationDetail BrandApplicationDetail { get; set; }
        public Concrete.Contact Contact { get; set; }
    }
}
