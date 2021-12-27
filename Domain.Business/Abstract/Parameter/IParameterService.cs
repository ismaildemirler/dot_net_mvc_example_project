using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Parameter;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Parameter
{
    public interface IParameterService
    {
        List<BrandClasses> GetAllBrandClasses();
        List<BrandSubClasses> GetAllBrandSubClasses();
        List<BrandSubClasses> GetBrandSubClassesByBrandClassId(int brandClassId);
        List<PrmFirmStatusType> GetAllFirmStatuTypes();
        List<City> GetAllCities();
        List<Town> GetAllTowns();        
        List<PrmTLDType> GetAllTLDTypes();
        PrmTLDType GetTLDType(RequestTLDType request);
        void UpdateTDLType(RequestTLDType request);
        void DeleteTLDType(RequestTLDType request);
        List<DomainPriceComplexType> GetDomainPriceList();
        DomainPrice GetDomainPrice(RequestDomainPrice request);
        void UpdateDomainPrice(RequestDomainPrice request);
        void DeleteDomainPrice(RequestDomainPrice request);
        List<PrmBlogCategory> GetBlogCategoryList();
        List<PrmBrandApplicationType> GetAllBrandApplicationTypes();
        List<PrmApplicationType> GetAllApplicationTypes();

        List<PrmTLDType> GetTldTypes();
    }
}
