
using System;

namespace Domain.Entity.Container.Request.Beneficiary
{
    public class RequestBeneficiary
    {
        public Guid BeneficiaryId { get; set; }
        public Guid BrandApplicationDetailId { get; set; }
        public Guid PatentApplicationDetailId { get; set; }
    }
}
