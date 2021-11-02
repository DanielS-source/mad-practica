using Microsoft.VisualStudio.TestTools.UnitTesting;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao.Tests
{
    [TestClass()]
    public class CommentsDaoEntityFrameworkTests
    {
        private const long userId = 123456;

        private TransactionScope transactionScope;
        private TestContext testContextInstance;

        private static IKernel kernel;
        private static ICommentsDao commentsDao;

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

        private Comments CreateComment(long imgId, long userId, String text, DateTime postDate) 
        {
            CommentsDao comment = new Comments
            {
                imgId = imgId,
                usrId = userId,
                message = text,
                postDate = postDate
            };

            commentsDao.create(comment);
            return comment;
        }

        #endregion 

        [TestMethod()]
        public void findByImageTest()
        {
            Image Image1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category_id);
            Image Image2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Pokemon", DateTime.Now, category_id_2);

            Comments Comment1 = CreateComment(Image1.imgId, userId, "Que chulo!", DateTime.Now);
            Comments Comment2 = CreateComment(Image1.imgId, userId, "No me gusta!", DateTime.Now);

            List<Comments> expectedComments = new List<Comments>(2);
            { 
                Comment1,
                Comment2
            };

            List<Comments> foundComments = commentsDao.findByImage(Image1.imgId);
            Assert.AreEqual(expectedComments, foundComments);

            foundComments = commentsDao.findByImage(Image2.imgId);
            Assert.AreEqual(0, foundComments.Count);

            Assert.Fail();
        }
    }
}