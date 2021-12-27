
using AutoMapper;
using Domain.Entity.Concrete;
using Domain.Entity.ViewModels.UserBrandApplication;
using Domain.Entity.ViewModels.UserBrandForWatching;
using Domain.Entity.ViewModels.UserIndustrialDesignApplication;
using Domain.Entity.ViewModels.UserPatentApplication;

namespace Domain.Business.Mapping.AutoMapper.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region BrandApplication
            CreateMap<Contact, ContactViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Beneficiary, BeneficiaryViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandApplicationDetail, BrandApplicationDetailViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<ContactViewModel, Contact>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BeneficiaryViewModel, Beneficiary>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandApplicationDetailViewModel, BrandApplicationDetail>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region BrandForWatchingApplication
            CreateMap<BrandForWatching, BrandForWatchingViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandWatchingApplicationDetail, BrandWatchingApplicationDetailViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BrandForWatchingViewModel, BrandForWatching>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandWatchingApplicationDetailViewModel, BrandWatchingApplicationDetail>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region OtherBrandApplication
            CreateMap<Beneficiary, BeneficiaryOtherViewModel>()
                .IncludeAllDerived()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Contact, ContactOtherViewModel>()
                .IncludeAllDerived()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandApplicationDetail, BrandApplicationDetailOtherViewModel>()
                .IncludeAllDerived()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BeneficiaryOtherViewModel, Beneficiary>()
                .IncludeAllDerived()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ContactOtherViewModel, Contact>()
                .IncludeAllDerived()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<BrandApplicationDetailOtherViewModel, BrandApplicationDetail>()
                .IncludeAllDerived()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region PatentApplication
            CreateMap<Beneficiary, BeneficiaryPatentViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap <PatentApplicationDetail, PatentApplicationDetailViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<BeneficiaryPatentViewModel, Beneficiary>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PatentApplicationDetailViewModel, PatentApplicationDetail>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion

            #region IndustrialDesignApplication
            CreateMap<IndustrialDesignApplicationDetail, IndustrialDesignApplicationDetailViewModel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<IndustrialDesignApplicationDetailViewModel, IndustrialDesignApplicationDetail>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            #endregion
        }
    }
}