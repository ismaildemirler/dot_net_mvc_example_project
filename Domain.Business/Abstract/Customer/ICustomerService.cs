using Domain.Entity.Container.Request.Customer;
using Domain.Entity.Container.Response.Customer;

namespace Domain.Business.Abstract.Customer
{
    public interface ICustomerService
    {
        ResponseCustomerApplication GetCustomerApplicationByRequest(RequestCustomerApplication request);
    }
}
