using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
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

            /*** UserService ***/
            kernel.Bind<IUserService>().To<UserService>();

            /*** ImageService ***/
            kernel.Bind<IImageService>().To<ImageService>();

            /*** UserProfileDao ***/
            kernel.Bind<IUserProfileDao>().To<UserProfileDaoEntityFramework>();

            /*** ImageDao ***/
            kernel.Bind<IImageDao>().To<ImageDaoEntityFramework>();

            /*** CommentsDao ***/
            kernel.Bind<ICommentsDao>().To<CommentsDaoEntityFramework>();

            /*** TagDao ***/
            kernel.Bind<ITagDao>().To<TagDaoEntityFramework>();

            /*** CategoryDao ***/
            kernel.Bind<ICategoryDao>().To<CategoryDaoEntityFramework>();

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