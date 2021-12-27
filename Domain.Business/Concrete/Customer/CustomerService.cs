using Domain.Business.Abstract.Customer;
using Domain.DataAccess.Abstract.Customer;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Customer;
using Domain.Entity.Container.Response.Customer;
using System;
using System.Linq;

namespace Domain.Business.Concrete.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAL _customerDAL;

        public CustomerService(ICustomerDAL customerDAL)
        {
            _customerDAL = customerDAL;
        }

        public ResponseCustomerApplication GetCustomerApplicationByRequest(RequestCustomerApplication request)
        {
            var query = _customerDAL.GetRepository<CustomerApplication>().Queryable();
            var response = new ResponseCustomerApplication();

            if (request.CustomerApplicationId != null && request.CustomerApplicationId != Guid.Empty)
                query = query.Where(p => p.CustomerApplicationId == request.CustomerApplicationId);

            var customerApplication = query.FirstOrDefault();
            if (customerApplication != null)
            {
                response.CustomerApplication = customerApplication;
            }
            return response;
        }
    }
}
