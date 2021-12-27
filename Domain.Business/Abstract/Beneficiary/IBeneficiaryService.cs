
using Domain.Entity.Container.Request.Beneficiary;
using Domain.Entity.Container.Response.Beneficiary;
using Domain.Entity.Container.Response.Bneficiary;

namespace Domain.Business.Abstract.Beneficiary
{
    public interface IBeneficiaryService
    {
        ResponseBeneficiary GetBeneficiaryByRequest(RequestBeneficiary request);
        ResponseBeneficiaryRegistry CreateBeneficiaryRegistry(RequestBeneficiaryRegistry request);
        ResponseBeneficiaryContactRegistry CreateBeneficiaryContactRegistry(RequestBeneficiaryContactRegistry request);
    }
}
