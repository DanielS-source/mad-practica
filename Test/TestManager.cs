using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using System.Configuration;
using System.Data.Entity;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    public class TestManager
    {
        /// <summary>
        /// Configures and populates the Ninject kernel
        /// </summary>
        /// <returns>The NInject kernel</returns>
        public static IKernel ConfigureNInjectKernel()
        {
            #region SourceCodeConfig

            IKernel kernel = new StandardKernel();

            kernel.Bind<ICommentsDao>().To<CommentsDaoEntityFramework>();
            kernel.Bind<IImageDao>().To<ImageDaoEntityFramework>();
            kernel.Bind<IUserProfileDao>().To<UserProfileDaoEntityFramework>();
            kernel.Bind<ICategoryDao>().To<CategoryDaoEntityFramework>();
            kernel.Bind<ITagDao>().To<TagDaoEntityFramework>();

            kernel.Bind<IImageService>().To<ImageService>().InSingletonScope();
            kernel.Bind<IUserService>().To<UserService>().InSingletonScope();

            string connectionString = ConfigurationManager.ConnectionStrings["photogramEntities"].ConnectionString;
            kernel.Bind<DbContext>().ToSelf().InSingletonScope().WithConstructorArgument("nameOrConnectionString", connectionString);

            #endregion SourceCodeConfig


            return kernel;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing or resetting unmanaged resources.
        /// </summary>
        /// <param name="kernel">The NInject kernel.</param>
        public static void ClearNInjectKernel(IKernel kernel)
        {
            kernel.Dispose();
        }
    }
}