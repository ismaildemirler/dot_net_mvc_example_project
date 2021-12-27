using Domain.Business.DependencyResolver.Ninject;
using Domain.Infrastructure.ElasticSearch;
using Domain.Infrastructure.Entities;
using Domain.Web.Infrastructure;
using Domain.WebHelpers.ModelBinders;
using Domain.WebHelpers.Models.Shared;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Domain.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Init();

            ElasticProvider<ErrorLog> elasticProvider = new ElasticProvider<ErrorLog>();
            elasticProvider.CreateIndex();

            AreaRegistration.RegisterAllAreas();
            
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ModelBinders.Binders.Add(typeof(DtParameterModel), new DtModelBinder());
            ModelBinders.Binders.Add(typeof(MDtParameterModel), new MDtModelBinder());
            
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactoryWebUI());
        }
    }
}
