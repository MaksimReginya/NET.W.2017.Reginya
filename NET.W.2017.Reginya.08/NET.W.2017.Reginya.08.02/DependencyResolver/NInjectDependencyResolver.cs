using BLL.Interface.ServiceInterface;
using BLL.ServiceImplementation;
using DAL.Fake;
using DAL.Interface;
using Ninject;

namespace DependencyResolver
{
    public class NInjectDependencyResolver
    {
        public static void Configure(IKernel kernel)
        {
            kernel.Bind<IBankAccountRepository>().To<FakeRepository>().WithConstructorArgument("filePath", "accounts");
            kernel.Bind<IAccountNumberGenerator>().To<AccountNumberGenerator>();

            var accountRepository = kernel.Get<IBankAccountRepository>();

            kernel.Bind<IBankAccountService>().To<BankAccountService>().WithConstructorArgument("repository", accountRepository);
        }
    }
}
