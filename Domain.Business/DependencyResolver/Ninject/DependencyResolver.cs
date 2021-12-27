using Domain.Business.DependencyResolver.Ninject;
using Ninject;

namespace KOSGEB.YKBS.Business.DependencyResolver.Ninject
{
    public class DependencyResolver<T>
    {
        public static T Resolve()
        {
            return Bootstrapper.Kernel.Get<T>();
        }
    }
}
