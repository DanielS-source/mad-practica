using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.LikeDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService;
using Ninject;
using System.Configuration;
using System.Data.Entity;

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
            //kernel.Bind<IFollowDao>().To<FollowDaoEntityFramework>();
            kernel.Bind<IImageDao>().To<ImageDaoEntityFramework>();
            //kernel.Bind<ILikeDao>().To<LikeDaoEntityFramework>();
            kernel.Bind<IUserProfileDao>().To<UserProfileDaoEntityFramework>();

            //kernel.Bind<ICommentService>().To<CommentService>().InSingletonScope(); -> ImageRelatedService
            //kernel.Bind<ILikeService>().To<LikeService>().InSingletonScope();  -> ImageRelatedService
            kernel.Bind<IImageService>().To<ImageService>().InSingletonScope();
            kernel.Bind<IUserRelatedService>().To<UserRelatedService>().InSingletonScope();
            kernel.Bind<IUserService>().To<UserService>().InSingletonScope();

            string connectionString = ConfigurationManager.ConnectionStrings["PracticaMaDEntities"].ConnectionString;
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