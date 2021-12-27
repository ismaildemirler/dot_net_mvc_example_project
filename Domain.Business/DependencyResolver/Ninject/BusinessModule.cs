using Domain.Business.Concrete.Attachment;
using Domain.Business.Concrete.Basket;
using Domain.Business.Concrete.Beneficiary;
using Domain.Business.Concrete.Blog;
using Domain.Business.Concrete.Brand;
using Domain.Business.Concrete.Common;
using Domain.Business.Concrete.Contact;
using Domain.Business.Concrete.Customer;
using Domain.Business.Concrete.Domain;
using Domain.Business.Concrete.IndustrialDesign;
using Domain.Business.Concrete.Log;
using Domain.Business.Concrete.Management;
using Domain.Business.Concrete.Menus;
using Domain.Business.Concrete.Parameter;
using Domain.Business.Concrete.Patent;
using Domain.Business.Concrete.ReferenceFirm;
using Domain.Business.Concrete.Service;
using Domain.Business.Concrete.Slider;
using Domain.DataAccess.Concrete.EntityFramework.Attachment;
using Domain.DataAccess.Concrete.EntityFramework.Beneficiary;
using Domain.DataAccess.Concrete.EntityFramework.Blog;
using Domain.DataAccess.Concrete.EntityFramework.Brand;
using Domain.DataAccess.Concrete.EntityFramework.Common;
using Domain.DataAccess.Concrete.EntityFramework.Contact;
using Domain.DataAccess.Concrete.EntityFramework.Customer;
using Domain.DataAccess.Concrete.EntityFramework.Domain;
using Domain.DataAccess.Concrete.EntityFramework.IndustrialDesign;
using Domain.DataAccess.Concrete.EntityFramework.Management;
using Domain.DataAccess.Concrete.EntityFramework.Menus;
using Domain.DataAccess.Concrete.EntityFramework.Patent;
using Domain.DataAccess.Concrete.EntityFramework.ReferenceFirm;
using Domain.DataAccess.Concrete.EntityFramework.Slider;
using Domain.DataAccess.Concrete.Parameter;
using Ninject.Extensions.Conventions;
using Ninject.Modules;
using Ninject.Web.Common;

namespace Domain.Business.DependencyResolver.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(bm =>
            {
                bm.FromAssemblyContaining<SystemUserDAL>().SelectAllClasses().InNamespaceOf<SystemUserDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<BrandDAL>().SelectAllClasses().InNamespaceOf<BrandDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<ParameterDAL>().SelectAllClasses().InNamespaceOf<ParameterDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<BeneficiaryDAL>().SelectAllClasses().InNamespaceOf<BeneficiaryDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<ManagementDAL>().SelectAllClasses().InNamespaceOf<ManagementDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<BlogDAL>().SelectAllClasses().InNamespaceOf<BlogDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<CustomerDAL>().SelectAllClasses().InNamespaceOf<CustomerDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<ContactDAL>().SelectAllClasses().InNamespaceOf<ContactDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<MenuDAL>().SelectAllClasses().InNamespaceOf<MenuDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<PatentDAL>().SelectAllClasses().InNamespaceOf<PatentDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<AttachmentDAL>().SelectAllClasses().InNamespaceOf<AttachmentDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<SliderDAL>().SelectAllClasses().InNamespaceOf<SliderDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<ReferenceFirmDAL>().SelectAllClasses().InNamespaceOf<ReferenceFirmDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<DomainDAL>().SelectAllClasses().InNamespaceOf<DomainDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());
                bm.FromAssemblyContaining<IndustrialDesignDAL>().SelectAllClasses().InNamespaceOf<IndustrialDesignDAL>().BindDefaultInterfaces().Configure(t => t.InRequestScope());


                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<LogService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<BasketService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<SystemUserService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<BrandService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<ParameterService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<BeneficiaryService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<ManagementService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<BlogService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<CustomerService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<ContactService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<MenuService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<AttachmentService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<PatentService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<SliderService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<ReferenceFirmService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<DomainService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<IndustrialDesignService>().BindAllInterfaces().Configure(t => t.InRequestScope());
                bm.FromThisAssembly().SelectAllClasses().InNamespaceOf<ServicesService>().BindAllInterfaces().Configure(t => t.InRequestScope());
            });
        }
    }
}
