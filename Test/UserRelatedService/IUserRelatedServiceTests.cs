using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Test;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService.Tests
{
    [TestClass()]
    public class IUserRelatedServiceTests
    {
       
        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;
        private static IKernel kernel;
        private static IUserService userService;
        private static IUserRelatedService userRelatedService;
        private static IImageService imageService;
        private static ICategoryDao categoryDao;
        private static ITagDao TagDao;

        private TransactionScope transaction;

        #region Test Configuration

        /// <summary>
        /// ClassInitialize run code before running the first test in the class.
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userService = kernel.Get<IUserService>();
            userRelatedService = kernel.Get<IUserRelatedService>();
            imageService = kernel.Get<IImageService>();
            categoryDao = kernel.Get<ICategoryDao>();
            TagDao = kernel.Get<ITagDao>();
        }

        /// <summary>
        /// ClassCleanup run code after all tests in a class have run.
        /// </summary>
        [ClassCleanup]
        public static void ClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        /// <summary>
        /// TestInitialize run code before running each test.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            transaction = new TransactionScope();
        }

        /// <summary>
        /// TestCleanup run code after each test has run.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            transaction.Dispose();
            ImageCache.Dispose();
        }

        #endregion Test Configuration

        #region Auxiliary Atributes
        private const string loginName = "loginNameTest";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";

        private Tag tag1 = new Tag()
        {
            name = "A Coruña",
            uses = 0
        };

        private Tag tag2 = new Tag()
        {
            name = "BM&W",
            uses = 0
        };

        private Tag tag3 = new Tag()
        {
            name = "Pokemon",
            uses = 0
        };

        #endregion

        #region Auxiliary Methods

        private Image CreateImage(long user, string path, string title, string description, DateTime date, long category)
        {
            Image image = new Image
            {
                usrId = user,
                pathImg = path,
                title = title,
                description = description,
                dateImg = date,
                catId = category
            };
            return image;
        }

        private Category category = new Category()
        {
            name = "Digimon"
        };

        private Follow CreateFollow (long userId, long follower)
        {
            Follow follow = new Follow
            {
                usrId = userId,
                followerId = follower
            };
            return follow;
        }

        private UserProfile CreateUser(long id, string login, string pass, string firstN, string lName, string mail, string lang)
        {
            UserProfile user = new UserProfile
            {
                usrId = id,
                loginName = login,
                enPassword = pass,
                firstName = firstN,
                lastName = lName,
                email = mail,
                language = lang
            };
            return user;
        }
        #endregion


        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region ViewUser
        /// <summary>
        ///A test for GetUserImagesTest
        ///</summary>
        
        [TestMethod]
        public void GetUserImagesTest()
        {

            int numberOfImages = 3;
            int count = 10;
            int startIndex = 0;

            ImageBlock imageBlock;

            /* Create User */
            var userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language));

            categoryDao.Create(category);

            /* Create and Add Images to User*/
            Image image_1 = CreateImage(userId, "C:/Software/DataBase/Images/Agumon", "Digimon", "Otro", DateTime.Now, category.catId);
            Image image_2 = CreateImage(userId, "C:/Software/DataBase/Images/MetalGreyMon", "Digimon", "Otro", DateTime.Now, category.catId);
            Image image_3 = CreateImage(userId, "C:/Software/DataBase/Images/WarGreyMon", "Digimon", "Otro", DateTime.Now, category.catId);

            TagDao.Create(tag1);
            TagDao.Create(tag2);
            TagDao.Create(tag3);

            IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

            imageService.PostImage(image_1, tagsId);
            imageService.PostImage(image_2, tagsId);
            imageService.PostImage(image_3, tagsId);

            /* Get the images in blocks of "count" size */
            do
            {
                imageBlock = userRelatedService.GetUserImages(
                    userId, startIndex, count);

                Assert.IsTrue(imageBlock.Images.Count <= count);

                startIndex += count;
            } while (imageBlock.ExistMoreImages);

                Assert.AreEqual(numberOfImages,
                startIndex - count + imageBlock.Images.Count);
        }

        [TestMethod]
        public void GetUserFollowersTest()
        {
            List<Follow> followers;

            /* Create User */
            var userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language));

            /* Create User Followers */
            var follower_1 = userService.RegisterUser("Follower1", clearPassword,
                new UserProfileDetails(firstName, lastName, "follower1@udc.es", language));
            var follower_2 = userService.RegisterUser("Follower2", clearPassword,
               new UserProfileDetails(firstName, lastName, "follower2@udc.es", language));

            /* Expected User Followers */
            Follow expected_follower1 = CreateFollow(userId, follower_1);
            Follow expected_follower2 = CreateFollow(userId, follower_2);

            userRelatedService.FollowUser(follower_1, userId);
            userRelatedService.FollowUser(follower_2, userId);

            List<Follow> expected_followers = new List<Follow>(2)
            {
                expected_follower1,
                expected_follower2
            };

            followers =
                userRelatedService.GetUserFollowers(userId);

            Assert.AreEqual(expected_followers.Count, followers.Count);
        }

        [TestMethod]
        public void GetUserFollowsTest()
        {
            List<UserProfile> follows;

            /* Create User */
            var userId = userService.RegisterUser(loginName, clearPassword,
                new UserProfileDetails(firstName, lastName, email, language));

            /* Create Users To Follow */
            var userToFollow_1 = userService.RegisterUser("UserToFollow1", clearPassword,
                new UserProfileDetails(firstName, lastName, "usertofollow1@udc.es", language));
            var userToFollow_2 = userService.RegisterUser("UserToFollow2", clearPassword,
               new UserProfileDetails(firstName, lastName, "usertofollow2@udc.es", language));
            var userToFollow_3 = userService.RegisterUser("UserToFollow3", clearPassword,
              new UserProfileDetails(firstName, lastName, "usertofollow3@udc.es", language));

            /* Expected Users To Follow */
            UserProfile expected_userToFollow1 = CreateUser(userToFollow_1, "UserToFollow1", 
                clearPassword, firstName, lastName, "usertofollow1@udc.es", language);
            UserProfile expected_userToFollow2 = CreateUser(userToFollow_2, "UserToFollow2", 
                clearPassword, firstName, lastName, "usertofollow2@udc.es", language);
            UserProfile expected_userToFollow3 = CreateUser(userToFollow_3, "UserToFollow3", 
                clearPassword, firstName, lastName, "usertofollow3@udc.es", language);

            userRelatedService.FollowUser(userId, userToFollow_1);
            userRelatedService.FollowUser(userId, userToFollow_2);
            userRelatedService.FollowUser(userId, userToFollow_3);

            List<UserProfile> expected_follows = new List<UserProfile>(3)
            {
                expected_userToFollow1,
                expected_userToFollow2,
                expected_userToFollow3
            };

            follows =
                userRelatedService.GetUserFollows(userId);

            Assert.AreEqual(expected_follows.Count, follows.Count);
        }
        #endregion

    }
}