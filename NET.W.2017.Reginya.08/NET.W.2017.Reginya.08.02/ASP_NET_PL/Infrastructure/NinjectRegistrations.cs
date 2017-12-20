using DependencyResolver;
using Ninject.Modules;

namespace ASP_NET_PL.Infrastructure
{
    public class NInjectRegistrations : NinjectModule
    {
        public override void Load() =>
            this.Kernel.ConfigurateResolverWeb();
    }
}