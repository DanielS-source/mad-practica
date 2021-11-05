using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService.Tests
{
    [TestClass()]
    public class IImageServiceTests
    {

        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";

        private static IKernel kernel;
        private static IImageService ImageService;
        private static IUserService userService;
        private static IUserRelatedService userRelatedService;
        private static IImageDao ImageDao;
        private static ICategoryDao catogoryDao;

        // Variables used in several tests are initialized here
        private const long NON_EXISTENT_USER_ID = -1;

        private TransactionScope transactionScope;

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            ImageService = kernel.Get<IImageService>();
            userRelatedService = kernel.Get<IUserRelatedService>();
            ImageDao = kernel.Get<IImageDao>();
            userService = kernel.Get<IUserService>();
            catogoryDao = kernel.Get<ICategoryDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transactionScope.Dispose();
            CacheManager.Dispose();
        }

        #endregion Additional test attributes

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
            name = "Pokemon"
        };

        private Category category2 = new Category()
        {
            name = "Otro"
        };

        #endregion

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the
        /// current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void PostImageTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language));

                catogoryDao.Create(category);

                Image Image = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                Image = ImageService.PostImage(Image);

                Image FoundImage = ImageDao.Find(Image.imgId);

                Assert.AreEqual(Image, FoundImage);
            }
        }

        [TestMethod()]
        public void SearchImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language));

                catogoryDao.Create(category);
                catogoryDao.Create(category2);

                Image Image1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                Image Image2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Pokemon", DateTime.Now, category2.catId);
                _ = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Otro", DateTime.Now, category2.catId);

                List<Image> imageList = new List<Image>(2)
                {
                    Image1,
                    Image2
                };

                Boolean existMoreImages = false;
                int startIndex = 0;
                int count = 10;
                ImageBlock expectedImages = new ImageBlock(imageList, existMoreImages);

                ImageBlock foundImages = ImageService.SearchImages("Pokemon", null, startIndex, count);

                Assert.AreEqual(expectedImages.Images, foundImages.Images);
            }
        }

        [TestMethod()]
        public void SearchFollowedImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language));

                var userId2 = userService.RegisterUser("loginName", clearPassword,
                    new UserProfileDetails(firstName, lastName, email + "e", language));

                userRelatedService.FollowUser(userId2, userId);

                catogoryDao.Create(category);
                catogoryDao.Create(category2);
                Image Image1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                _ = CreateImage(userId2, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Pokemon", DateTime.Now, category2.catId);
                Image Image2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Otro", DateTime.Now, category.catId);

                List<Image> images = new List<Image>(2)
                {
                    Image1,
                    Image2
                };

                Boolean existMoreImages = false;
                int count = 10;
                int startIndex = 0;

                ImageBlock expectedImages = new ImageBlock(images, existMoreImages);

                ImageBlock foundImages = ImageService.SearchFollowedImages(userId2, startIndex, count);

                Assert.AreEqual(expectedImages.Images, foundImages.Images);
            }
        }
    }
}