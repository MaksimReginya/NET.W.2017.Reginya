using System.Data.Entity;
using BLL.Interface.MailServiceInterface;
using BLL.Interface.ServiceInterface;
using BLL.ServiceImplementation;
using DAL.EF;
using ORM;
using DAL.Interface;
using Ninject;

namespace DependencyResolver
{
    public class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            // kernel.Bind<IBankAccountRepository>().To<FakeRepository>().WithConstructorArgument("filePath", "accounts");
            kernel.Bind<IBankAccountRepository>().To<DatabaseRepository>().InSingletonScope();
            kernel.Bind<IAccountNumberGenerator>().To<AccountNumberGenerator>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            kernel.Bind<IMailService>().To<GmailService>().InSingletonScope();
            kernel.Bind<DbContext>().To<AccountContext>().InSingletonScope();

            var accountRepository = kernel.Get<IBankAccountRepository>();
            var unitOfWork = kernel.Get<IUnitOfWork>();            
            kernel
                .Bind<IBankAccountService>()
                .To<BankAccountService>()
                .WithConstructorArgument("repository", accountRepository)
                .WithConstructorArgument("unitOfWork", unitOfWork);
        }
    }
}
