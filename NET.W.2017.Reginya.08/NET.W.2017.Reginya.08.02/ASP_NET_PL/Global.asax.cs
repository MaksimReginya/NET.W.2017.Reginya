using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ASP_NET_PL.Infrastructure;
using Ninject;
using Ninject.Web.Mvc;

namespace ASP_NET_PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var registrations = new NInjectRegistrations();
            var kernel = new StandardKernel(registrations);
            kernel.Unbind<ModelValidatorProvider>();
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        protected void Application_Error(object sender, EventArgs e)
        {                        
            Server.Transfer("Error.html");
            Server.ClearError();
        }
    }
}
