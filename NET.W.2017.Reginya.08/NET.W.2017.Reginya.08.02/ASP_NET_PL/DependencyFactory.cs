using BLL.Interface.ServiceInterface;
using BLL.Interface.MailServiceInterface;
using DependencyResolver;
using Ninject;

namespace ASP_NET_PL
{
    public static class DependencyFactory
    {
        static DependencyFactory()
        {
            var ninjectKernel = new StandardKernel();
            NInjectDependencyResolver.Configure(ninjectKernel);

            BankAccountService = ninjectKernel.Get<IBankAccountService>();
            NumberGenerator = ninjectKernel.Get<IAccountNumberGenerator>();
            MailService = ninjectKernel.Get<IMailService>();
        }

        public static IBankAccountService BankAccountService { get; }

        public static IAccountNumberGenerator NumberGenerator { get; }

        public static IMailService MailService { get; }
    }
}