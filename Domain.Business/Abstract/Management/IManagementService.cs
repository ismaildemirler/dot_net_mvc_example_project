using Domain.Entity.ComplexType;
using Domain.Entity.Container.Request.Manage;

namespace Domain.Business.Abstract.Management
{
    public interface IManagementService
    {
        string GetAboutPageContent();
        void SaveAboutPageContent(RequestManagement request);

        ContactPageComplexType GetContactPageInformation();
        void SaveContactPageInformation(RequestManagement request);
    }
}
