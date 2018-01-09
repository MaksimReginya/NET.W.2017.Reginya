using DependencyResolver;
using Ninject.Modules;

namespace PL.ASP_NET.Infrastructure
{
    public class NInjectRegistrations : NinjectModule
    {
        public override void Load() =>
            this.Kernel.ConfigurateResolverWeb();
    }
}