using Castle.MicroKernel;
using Es.Udc.DotNet.MiniBank.Model.AccountDao;
using Es.Udc.DotNet.MiniBank.Model.AccountOperationDao;
using Es.Udc.DotNet.MiniBank.Model.AccountService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Ninject;
using System.Configuration;
using System.Data.Entity;

namespace Es.Udc.DotNet.MiniBank.HTTP.Util.IoC
{
    internal class IoCManagerNinject : IIoCManager
    {
        private static IKernel kernel;
        private static NinjectSettings settings;

        public void Configure()
        {
            settings = new NinjectSettings() { LoadExtensions = true };
            kernel = new StandardKernel(settings);

            /*** AccountService ***/
            kernel.Bind<IAccountService>().To<AccountService>();

            /*** AccountDao ***/
            kernel.Bind<IAccountDao>().
                To<AccountDaoEntityFramework>();

            /*** AccountDaoEntityFramework ***/
            kernel.Bind<IAccountOperationDao>().
                To<AccountOperationDaoEntityFramework>();

            /*** DbContext ***/
            string connectionString =
                ConfigurationManager.ConnectionStrings["MiniBankEntities"].ConnectionString;

            kernel.Bind<DbContext>().
                    ToSelf().
                    InSingletonScope().
                    WithConstructorArgument("nameOrConnectionString", connectionString);
        }

        public T Resolve<T>()
        {
            return kernel.Get<T>();
        }
    }
}