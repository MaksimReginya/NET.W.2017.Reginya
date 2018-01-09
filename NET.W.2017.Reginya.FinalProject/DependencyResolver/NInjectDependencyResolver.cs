using System.Data.Entity;
using BLL.Interface.MailServiceInterface;
using BLL.Interface.ServiceInterface;
using BLL.ServiceImplementation;
using DAL;
using DAL.Interface;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class NInjectDependencyResolver
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        public static void Configure(IKernel kernel, bool isWeb)
        {                     
            if (isWeb)
            {                
                kernel.Bind<DbContext>().To<AccountContext>().InRequestScope();                
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();                
            }
            else
            {                
                kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();             
            }

            kernel.Bind<IMailService>().To<GmailService>();
            kernel.Bind<IBankAccountRepository>().To<BankAccountRepository>();             
            kernel.Bind<IAccountOwnerRepository>().To<AccountOwnerRepository>();               
            kernel.Bind<IAccountNumberGenerator>().To<AccountNumberGenerator>();                                                     
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IBankManageService>().To<BankManageService>();
        }                        
    }
}