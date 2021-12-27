using Domain.Business.Abstract.Parameter;
using Domain.DataAccess.Abstract.Parameter;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.BrandClass;
using Domain.Entity.Container.Request.Domain;
using Domain.Entity.Container.Request.Parameter;
using Domain.Entity.Enum;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Parameter
{
    public class ParameterService : IParameterService
    {
        private readonly IParameterDAL _parameterDal;

        public ParameterService(IParameterDAL parameterDal)
        {
            _parameterDal = parameterDal;
        }

        public List<PrmFirmStatusType> GetAllFirmStatuTypes()
        {
            return _parameterDal.GetRepository<PrmFirmStatusType>().Queryable().ToList();
        }

        public List<BrandClasses> GetAllBrandClasses()
        {
            return _parameterDal.GetRepository<BrandClasses>().Queryable().ToList();
        }
        public List<BrandSubClasses> GetAllBrandSubClasses()
        {
            return _parameterDal.GetRepository<BrandSubClasses>().Queryable().ToList();
        }
        public List<BrandSubClasses> GetBrandSubClassesByBrandClassId(int brandClassId)
        {
            return _parameterDal.GetRepository<BrandSubClasses>().Queryable().Where(f=>f.BrandClassesId == brandClassId).ToList();
        }

        public List<City> GetAllCities()
        {
            return _parameterDal.GetRepository<City>().Queryable().ToList();
        }

        public List<Town> GetAllTowns()
        {
            return _parameterDal.GetRepository<Town>().Queryable().ToList();
        }

        public List<PrmTLDType> GetAllTLDTypes()
        {
            return _parameterDal.GetRepository<PrmTLDType>().Queryable().Where(w => w.StateId == (byte)EnumState.Active).ToList();
        }

        public PrmTLDType GetTLDType(RequestTLDType request)
        {
            var tldType = _parameterDal.GetRepository<PrmTLDType>().Queryable().Where(w => w.StateId == (byte)EnumState.Active && w.TLDTypeId == request.TLDTypeId).FirstOrDefault();
            if (tldType == null)
                throw new BusinessException("Kayıt bulunamadı!");

            return tldType;
        }

        public void UpdateTDLType(RequestTLDType request)
        {
            var rep = _parameterDal.GetRepository<PrmTLDType>();

            if (request.TLDTypeId > 0)
            {
                var tldType = rep.Queryable().Where(w => w.StateId == (byte)EnumState.Active && w.TLDTypeId == request.TLDTypeId).FirstOrDefault();

                if (tldType == null)
                    throw new BusinessException("Kayıt bulunamadı!");
                
                tldType.Description = request.Description;
                rep.Update(tldType);
            }
            else
            {
                PrmTLDType entity = new PrmTLDType() { 
                    Description = request.Description,
                    StateId = (byte)EnumState.Active
                };

                rep.Insert(entity);
            }
        }
        public void DeleteTLDType(RequestTLDType request)
        {
            var rep = _parameterDal.GetRepository<PrmTLDType>();
            rep.Delete(request.TLDTypeId);
        }
        
        public List<DomainPriceComplexType> GetDomainPriceList()
        {
            return _parameterDal.GetDomainPriceList();
        }

        public DomainPrice GetDomainPrice(RequestDomainPrice request)
        {
            var domainPrice = _parameterDal.GetRepository<DomainPrice>().Queryable().Where(w => w.StateId == (byte)EnumState.Active && w.DomainPriceId == request.DomainPriceId).FirstOrDefault();
            if (domainPrice == null)
                throw new BusinessException("Kayıt bulunamadı!");

            return domainPrice;
        }

        public List<PrmTLDType> GetTldTypes()
        {
            var tldType = _parameterDal.GetRepository<PrmTLDType>().Queryable().ToList();
            if (tldType == null)
                throw new BusinessException("Kayıt bulunamadı!");

            return tldType;
        }

        public void UpdateDomainPrice(RequestDomainPrice request)
        {
            var rep = _parameterDal.GetRepository<DomainPrice>();

            if (request.DomainPrice.DomainPriceId > 0)
            {
                var domainPrice = rep.Queryable().Where(w => w.StateId == (byte)EnumState.Active && w.DomainPriceId == request.DomainPrice.DomainPriceId).FirstOrDefault();

                if (domainPrice == null)
                    throw new BusinessException("Kayıt bulunamadı!");

                domainPrice.RefreshPrice = request.DomainPrice.RefreshPrice;
                domainPrice.RefreshPriceWithDiscount = request.DomainPrice.RefreshPriceWithDiscount;

                domainPrice.RegisterPrice = request.DomainPrice.RegisterPrice;
                domainPrice.RegisterPriceWithDiscount = request.DomainPrice.RegisterPriceWithDiscount;

                domainPrice.TransferPrice = request.DomainPrice.TransferPrice;
                domainPrice.TransferPriceWithDiscount = request.DomainPrice.TransferPriceWithDiscount;

                domainPrice.TLDTypeId = request.DomainPrice.TLDTypeId;

                rep.Update(domainPrice);
            }
            else
            {
                if (rep.Queryable().Any(w => w.TLDTypeId == request.DomainPrice.TLDTypeId && w.StateId == (byte)EnumState.Active))
                    throw new BusinessException("Seçtiğiniz domain uzantısına ait aktif kayıt bulunmaktadır. Lütfen aktif kaydı güncelleyiniz.");

                request.DomainPrice.StateId = (byte)EnumState.Active;
                rep.Insert(request.DomainPrice);
            }
        }

        public void DeleteDomainPrice(RequestDomainPrice request)
        {
            var rep = _parameterDal.GetRepository<DomainPrice>();
            rep.Delete(request.DomainPriceId);
        }

        public List<PrmBlogCategory> GetBlogCategoryList()
        {
            return _parameterDal.GetRepository<PrmBlogCategory>().Queryable().Where(w => w.StateId == (byte)EnumState.Active).ToList();
        }

        public List<PrmBrandApplicationType> GetAllBrandApplicationTypes()
        {
            return _parameterDal.GetRepository<PrmBrandApplicationType>().Queryable().ToList();
        }

        public List<PrmApplicationType> GetAllApplicationTypes()
        {
            return _parameterDal.GetRepository<PrmApplicationType>().Queryable().ToList();
        }
    }
}
