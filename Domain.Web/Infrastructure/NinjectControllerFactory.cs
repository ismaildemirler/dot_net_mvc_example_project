using Domain.Business.DependencyResolver.Ninject;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domain.Web.Infrastructure
{
    public class NinjectControllerFactoryWebUI : DefaultControllerFactory
    {
        public NinjectControllerFactoryWebUI()
        {

        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            #region Thread Principle
            Domain.Infrastructure.CrossCuttingConcerns.Security.Web.SecurityUtilities.SetCookieToPrincipal();
            #endregion

            var kernel=Bootstrapper.Kernel;
            return controllerType == null ? null : (IController)kernel.Get(controllerType);

        }

        public override void ReleaseController(IController controller)
        {
            Bootstrapper.Kernel.Release(controller);
            base.ReleaseController(controller);
        }
    }
}