using Ninject;

namespace Domain.Business.DependencyResolver.Ninject
{
    public class Bootstrapper
    {
        static Bootstrapper()
        {

        }
        public static IKernel Kernel = null;

        public static void Init(bool clientSide = true, bool? forceSOA = null)
        {
            Kernel = new StandardKernel();
            Kernel.Load(new AutoMapperModule());
            Kernel.Load(new BusinessModule());
        }
    }
}
