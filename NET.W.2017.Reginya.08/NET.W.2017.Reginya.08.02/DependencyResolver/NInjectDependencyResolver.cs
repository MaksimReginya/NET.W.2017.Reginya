using System.Data.Entity;
using BLL.Interface.MailServiceInterface;
using BLL.Interface.ServiceInterface;
using BLL.ServiceImplementation;
using DAL.EF;
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
                kernel.Bind<IMailService>().To<GmailService>().InRequestScope();
                kernel.Bind<DbContext>().To<AccountContext>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();                
            }            

            kernel.Bind<IBankAccountRepository>().To<DatabaseRepository>();
            kernel.Bind<IAccountNumberGenerator>().To<AccountNumberGenerator>();
                                   
            var accountRepository = kernel.Get<IBankAccountRepository>();
            kernel
                .Bind<IBankAccountService>()
                .To<BankAccountService>()
                .WithConstructorArgument("repository", accountRepository);
        }
    }
}
