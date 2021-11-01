using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Ninject;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Test;
using System.Transactions;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao.Tests
{
    [TestClass()]
    public class ImageDaoEntityFrameworkTests
    {
        private static IKernel kernel;
        private static IImageDao imageDao;

        // Variables used in several tests are initialized here
        private const long userId = 123456;
        private const long NON_EXISTENT_USER_ID = -2;
        private const long category_id = 1;
        private const long category_id_2 = 2;
        private TransactionScope transactionScope;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();
            imageDao = kernel.Get<IImageDao>();
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
            imageDao.Create(image);
            return image;
        }

        #endregion 

        [TestMethod()]
        public void FindByKeywordsAndCategoryTest()
        {

            Image Image1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category_id);
            Image Image2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Pokemon", DateTime.Now, category_id_2);
            _ = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Otro", DateTime.Now, category_id);

            List<Image> expectedImages = new List<Image>(2)
            {
                Image1,
                Image2
            };

            int count = 10;
            int startIndex = 0;

            List<Image> foundImages = imageDao.FindByKeywordsAndCategory("Pokemon", null, startIndex, count);

            Assert.AreEqual(expectedImages, foundImages);
        }

        [TestMethod()]
        public void FindByDateTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void FindByFollowedTest()
        {
            Assert.Fail();
        }
    }
}