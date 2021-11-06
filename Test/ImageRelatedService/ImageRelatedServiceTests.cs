using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.ImageRelatedService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.LikeDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageRelatedService.Tests
{
    [TestClass()]
    public class ImageRelatedServiceTests
    {

        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";

        private static IKernel kernel;
        private static IImageRelatedService imageRelatedService;
        private static IImageService imageService;
        private static ILikeDao likeDao;
        private static ICommentsDao commentsDao;
        private static IUserService userService;
        private static ICategoryDao categoryDao;

        private TransactionScope transactionScope;

        #region Additional test attributes

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext) 
        { 
            kernel = TestManager.ConfigureNInjectKernel();
            imageRelatedService = kernel.Get<IImageRelatedService>();
            imageService = kernel.Get<IImageService>();
            userService = kernel.Get<IUserService>();
            categoryDao = kernel.Get<ICategoryDao>();
            commentsDao = kernel.Get<ICommentsDao>();
            likeDao = kernel.Get<ILikeDao>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            transactionScope = new TransactionScope();
        }

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
            return image;
        }

        private Comments CreateComment(long userId, long imageId, string text) 
        {
            Comments Comment = new Comments
            {
                usrId = userId,
                imgId = imageId,
                message = text,
                postDate = DateTime.Now
            };
            return Comment;
        }

        private Category category = new Category()
        {
            name = "Pokemon"
        };

        private Category category2 = new Category()
        {
            name = "Otro"
        };

        #endregion Auxiliary Methods

        [TestMethod()]
        public void LikeImageTest()
        {
            using (var scope = new TransactionScope()) 
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                            new UserProfileDetails(firstName, lastName, email, language));
               
                categoryDao.Create(category);

                Image Image = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                Image = imageService.PostImage(Image);

                Likes Like = new Likes
                {
                    usrId = userId,
                    imgId = Image.imgId
                };

                Like = imageRelatedService.LikeImage(Like);

                var FoundLike = likeDao.Find(Like.likeId);

                Assert.AreEqual(Like, FoundLike);
               
            }   
        }

        [TestMethod()]
        public void AddCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                             new UserProfileDetails(firstName, lastName, email, language));

                categoryDao.Create(category);

                Image Image = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                Image = imageService.PostImage(Image);

                Comments Comment = CreateComment(userId, Image.imgId, "Me gusta esta imagen!");

                Comment = imageRelatedService.AddComment(Comment);

                Comments FoundComment = commentsDao.Find(Comment.comId);

                Assert.AreEqual(Comment, FoundComment);
            }
        }

        [TestMethod()]
        public void EditCommentTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                             new UserProfileDetails(firstName, lastName, email, language));
                categoryDao.Create(category);
                Image Image = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                Image = imageService.PostImage(Image);
                Comments Comment = CreateComment(userId, Image.imgId, "Me gusta esta imagen!");

                Comment = imageRelatedService.AddComment(Comment);
                imageRelatedService.EditComment(Comment.comId, "Ya no me gusta tanto...");

                Assert.AreEqual(Comment.message, "Ya no me gusta tanto...");
            }
        }

        [TestMethod()]
        public void GetImageRelatedCommentsTest()
        {
            using (var scope = new TransactionScope()) 
            { 
                var userId = userService.RegisterUser(loginName, clearPassword,
                            new UserProfileDetails(firstName, lastName, email, language));

                categoryDao.Create(category);
                categoryDao.Create(category2);
                Image Image1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                Image Image2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Otro", DateTime.Now, category.catId);

                Image1 = imageService.PostImage(Image1);
                Image2 = imageService.PostImage(Image2);

                Comments Comment1 = CreateComment(userId, Image1.imgId, "Me gusta esta imagen!");
                Comment1 = imageRelatedService.AddComment(Comment1);

                Comments Comment2 = CreateComment(userId, Image1.imgId, "A mi también!");
                Comment2 = imageRelatedService.AddComment(Comment2);

                Comments Comment3 = CreateComment(userId, Image2.imgId, "A mi también!");
                Comment3 = imageRelatedService.AddComment(Comment3);

                List<Comments> ExpectedFromImage1 = new List<Comments>(2)
                {
                    Comment1,
                    Comment2
                };

                List<Comments> ExpectedFromImage2 = new List<Comments>(1)
                {
                    Comment3
                };

                List<Comments> foundComments1 = imageRelatedService.GetImageRelatedComments(Image1.imgId);
                List<Comments> foundComments2 = imageRelatedService.GetImageRelatedComments(Image2.imgId);

                Assert.AreEqual(ExpectedFromImage1.Count, foundComments1.Count);
                Assert.AreEqual(ExpectedFromImage2.Count, foundComments2.Count);
            }
        }
    }
}