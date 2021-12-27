using Domain.Entity.Concrete;
using System.Collections.Generic;
using Domain.Infrastructure.Aspects.Postsharp.CacheAspects;
using Domain.Infrastructure.CrossCuttingConcerns.Caching.Microsoft;
using KOSGEB.YKBS.Business.DependencyResolver.Ninject;
using Domain.Business.Abstract.Parameter;
using Domain.Business.Abstract.Management;
using Domain.Entity.ComplexType;
using Domain.Business.Abstract.Blog;
using Domain.Business.Abstract.Menus;
using Domain.Business.Abstract.Slider;
using Domain.Business.Abstract.ReferenceFirm;
using Domain.Business.Abstract.Domain;
using System.Linq;
using Domain.Entity.Container.Response.Services;
using Domain.Business.Abstract.Service;

namespace Domain.WebHelpers.Infrastructre
{
    public static class CacheManager
    {
        #region  BrandClasses

        public static int brandClassId { get; set; }

        public static List<BrandClasses> GetAllBrandClasses
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetAllBrandClasses();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<BrandSubClasses> GetAllBrandSubClasses
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetAllBrandSubClasses();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<BrandSubClasses> GetBrandSubClassesByClassId
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetBrandSubClassesByBrandClassId(brandClassId);
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        #region Brand
        public static List<PrmBrandApplicationType> GetAllBrandApplicationTypes
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parameterService = DependencyResolver<IParameterService>.Resolve();
                return parameterService.GetAllBrandApplicationTypes();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        #region FirmStatuTYpes
        public static List<PrmFirmStatusType> GetAllFirmStatuTypes
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetAllFirmStatuTypes();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        #region City, Town
        public static List<City> GetAllCities
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetAllCities();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<Town> GetAllTowns
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parametreService = DependencyResolver<IParameterService>.Resolve();
                return parametreService.GetAllTowns();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        public static ContactPageComplexType GetManagement
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var managementService = DependencyResolver<IManagementService>.Resolve();
                return managementService.GetContactPageInformation();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<PrmBlogCategory> GetBlogCategories
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parameterService = DependencyResolver<IParameterService>.Resolve();
                return parameterService.GetBlogCategoryList();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<BlogContentComplexType> GetRecentBlogs
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var blogService = DependencyResolver<IBlogService>.Resolve();
                return blogService.GetRecentBlogs();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        #region Menus
        public static List<Menu> GetAllMenus
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var menuService = DependencyResolver<IMenuService>.Resolve();
                return menuService.GetAllMenus()?.Menus;
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<SubMenu> GetAllSubMenus
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var menuService = DependencyResolver<IMenuService>.Resolve();
                return menuService.GetAllSubMenus()?.SubMenus;
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        #region Domain
        public static List<PrmTLDType> GetAllTldType
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parameterService = DependencyResolver<IParameterService>.Resolve();
                return parameterService.GetTldTypes();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<DomainPrice> GetAllDomainPrices
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var domainService = DependencyResolver<IDomainService>.Resolve();
                return domainService.GetDomainPrices();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
        #endregion

        public static List<PrmApplicationType> GetAllApplicationTypes
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var parameterService = DependencyResolver<IParameterService>.Resolve();
                return parameterService.GetAllApplicationTypes();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<SliderContent> GetAllSliderContent
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var sliderService = DependencyResolver<ISliderService>.Resolve();
                return sliderService.GetActiveSliderContents();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<SliderContentDetail> GetAllSliderContentDetail
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var sliderService = DependencyResolver<ISliderService>.Resolve();
                return sliderService.GetActiveSliderContentDetails();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<ReferenceFirm> GetAllReferenceFirms
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var referenceService = DependencyResolver<IReferenceFirmService>.Resolve();
                return referenceService.GetActiveReferenceFirms();
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }

        public static List<ResponseService> GetServices
        {
            [CacheAspect(typeof(MemoryCacheManager))]
            get
            {
                var service = DependencyResolver<IServicesService>.Resolve();
                return service.GetAllServices(true);
            }
            [CacheRemoveAspect(typeof(MemoryCacheManager))]
            set
            {
                var a = value;
            }
        }
    }
}
