using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;

namespace Domain.Infrastructure.Utilities.MVC.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;
        public NinjectControllerFactory(params INinjectModule[] module)
        {
            _kernel = new StandardKernel(module);
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            #region Thread Principle
            CrossCuttingConcerns.Security.Web.SecurityUtilities.SetCookieToPrincipal();
            #endregion
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }
    }
}
