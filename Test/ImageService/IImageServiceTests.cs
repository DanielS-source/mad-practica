using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Transactions;
using System.Collections.Generic;
using Ninject;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService.Tests
{
    [TestClass()]
    public class IImageServiceTests
    {

        private const string loginName = "loginNameTest";

        private const string clearPassword = "password";
        private const string firstName = "nameTest";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";

        private static IKernel kernel;
        private static IImageService ImageService;
        private static IUserService userService;
        private static IImageDao ImageDao;
        private static ITagDao TagDao;
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
            ImageDao = kernel.Get<IImageDao>();
            userService = kernel.Get<IUserService>();
            catogoryDao = kernel.Get<ICategoryDao>();
            TagDao = kernel.Get<ITagDao>();
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
            ImageCache.Dispose();
        }

        private Tag tag1 = new Tag()
        {
            name = "A Coruña",
            uses = 0L
        };

        private Tag tag2 = new Tag()
        {
            name = "BM&W",
            uses = 0L
        };

        private Tag tag3 = new Tag()
        {
            name = "Pokemon",
            uses = 0L
        };
        #endregion Additional test attributes

        #region Auxiliary Methods

        private ImageDTO CreateImage(long user, string path, string title, string description, DateTime date, long category)
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
            return new ImageDTO(image, null);
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
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                Image Image = ImageService.PostImage(ImageDTO, tagsId);
                Image FoundImage = ImageDao.Find(Image.imgId);

                Assert.AreEqual(Image, FoundImage);
                Assert.AreEqual(3, Image.Tag.Count);
                Assert.IsTrue(Image.Tag.Contains(tag1));
                Assert.IsTrue(Image.Tag.Contains(tag2));
                Assert.IsTrue(Image.Tag.Contains(tag3));
            }
        }

        [TestMethod()]
        public void PostImageWithoutTagsTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                Image Image = ImageService.PostImage(ImageDTO, new List<long>());
                Image FoundImage = ImageDao.Find(Image.imgId);

                Assert.AreEqual(Image, FoundImage);
                Assert.AreEqual(0, Image.Tag.Count);
            }
        }

        [TestMethod()]
        public void SearchImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);
                catogoryDao.Create(category2);

                ImageDTO ImageDTO1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                ImageDTO ImageDTO2 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Pokemon", DateTime.Now, category2.catId);
                ImageDTO ImageDTO3 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Otro", "Otro", DateTime.Now, category2.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                Image Image1 = ImageService.PostImage(ImageDTO1, tagsId);
                Image Image2 = ImageService.PostImage(ImageDTO2, tagsId);
                Image Image3 = ImageService.PostImage(ImageDTO3, tagsId);

                Image foundImage = ImageDao.Find(Image1.imgId);

                List<Image> imageList = new List<Image>(2)
                {
                    Image1,
                    Image2
                };

                bool existMoreImages = false;
                int startIndex = 0;
                int count = 10;
                ImageBlock expectedImages = new ImageBlock(imageList, existMoreImages);

                SearchImageBlock foundImages = ImageService.SearchImages("Pokemon", null, startIndex, count);

                Assert.AreEqual(expectedImages.Images.Count, foundImages.Images.Count);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteImageTest()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);


                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                Image Image = ImageService.PostImage(ImageDTO, tagsId);

                ImageService.DeleteImage(Image.imgId);

                ImageDao.Find(Image.imgId);

            }
        }


        [TestMethod()]
        public void FindByUserWithTags()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                Image Image = ImageService.PostImage(ImageDTO, tagsId);
                Image FoundImage = ImageDao.FindByUserWithTags(userId);

                Assert.AreEqual(tagsId[0], FoundImage.Tag.ToList()[0].tagId);
                Assert.AreEqual(tagsId[1], FoundImage.Tag.ToList()[1].tagId);
                Assert.AreEqual(tagsId[2], FoundImage.Tag.ToList()[2].tagId);
            }
        }
        //List<Image> FindByTag(long tagId, int startIndex, int count);
        [TestMethod()]
        public void FindByTag()
        {
            int startIndex = 0;
            int count = 10;

            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDao1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                ImageDTO ImageDao2 = CreateImage(userId, "C:/Software/DataBase/Images/Pikachu", "Pokemon", "Otro", DateTime.Now, category.catId);
                ImageDTO ImageDao3 = CreateImage(userId, "C:/Software/DataBase/Images/Charmander", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                IList<long> tagsId2 = new List<long>
                {
                    tag3.tagId
                };

                IList<long> tagsId3 = new List<long>
                {
                    tag1.tagId
                };

                Image Image1 = ImageService.PostImage(ImageDao1, tagsId);
                Image Image2 = ImageService.PostImage(ImageDao2, tagsId2);
                Image Image3 = ImageService.PostImage(ImageDao3, tagsId3);

                List<Image> FoundImage = ImageDao.FindByTag(count, startIndex, tag1.tagId).ToList();
                Assert.AreEqual(Image1.pathImg, FoundImage[0].pathImg);
                Assert.AreEqual(tag1.tagId, FoundImage[0].Tag.ToList()[0].tagId);
                Assert.AreEqual(tag1.tagId, FoundImage[1].Tag.ToList()[0].tagId);

                List<Image> FoundImage_2 = ImageDao.FindByTag(count, startIndex, tag1.tagId).ToList();
                Assert.AreEqual(Image2.pathImg, FoundImage_2[1].pathImg);
                Assert.AreEqual(tag3.tagId, FoundImage_2[1].Tag.ToList()[0].tagId);

            }
        }

        [TestMethod()]
        public void FindImagesByTag()
        {
            int startIndex = 0;
            int count = 10;

            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
                ImageDTO ImageDTO2 = CreateImage(userId, "C:/Software/DataBase/Images/Pikachu", "Pokemon", "Otro", DateTime.Now, category.catId);
                ImageDTO ImageDTO3 = CreateImage(userId, "C:/Software/DataBase/Images/Charmeleon", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId1 = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                IList<long> tagsId2 = new List<long>
                {
                    tag1.tagId,
                    tag3.tagId
                };

                IList<long> tagsId3 = new List<long>
                {
                    tag2.tagId,
                    tag3.tagId
                };


                Image Image1 = ImageService.PostImage(ImageDTO1, tagsId1);
                Image Image2 = ImageService.PostImage(ImageDTO2, tagsId2);
                Image Image3 = ImageService.PostImage(ImageDTO3, tagsId3);

                ImagePageable FoundImages = ImageService.FindImagesByTagPageable(count, startIndex, tag1.tagId);
                Assert.AreEqual(2, FoundImages.ImageWithTagsDTO.Count);
                Assert.AreEqual(Image1.pathImg, FoundImages.ImageWithTagsDTO[0].pathImg);
                Assert.AreEqual(tag1.tagId, FoundImages.ImageWithTagsDTO[0].Tags.ToList()[0].TagId);
                Assert.AreEqual(tag1.tagId, FoundImages.ImageWithTagsDTO[1].Tags.ToList()[0].TagId);

                ImagePageable FoundImages_2 = ImageService.FindImagesByTagPageable(count, startIndex, tag3.tagId);
                Assert.AreEqual(3, FoundImages_2.ImageWithTagsDTO.Count);
                Assert.AreEqual(Image1.pathImg, FoundImages_2.ImageWithTagsDTO[0].pathImg);
                Assert.AreEqual(tag3.tagId, FoundImages_2.ImageWithTagsDTO[0].Tags.ToList()[2].TagId);
                Assert.AreEqual(tag3.tagId, FoundImages_2.ImageWithTagsDTO[1].Tags.ToList()[1].TagId);
                Assert.AreEqual(tag3.tagId, FoundImages_2.ImageWithTagsDTO[2].Tags.ToList()[1].TagId);
            }
        }

        [TestMethod()]
        public void UpdateImage()
        {
            using (var scope = new TransactionScope())
            {
                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                catogoryDao.Create(category);

                ImageDTO ImageDTO = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);

                TagDao.Create(tag1);
                TagDao.Create(tag2);
                TagDao.Create(tag3);

                IList<long> tagsId = new List<long>
                {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
                };

                IList<long> updated_tagsId = new List<long>();

                Image Image = ImageService.PostImage(ImageDTO, tagsId);
                Image FoundImage = ImageDao.Find(Image.imgId);

                Assert.AreEqual(Image, FoundImage);
                Assert.AreEqual(3, Image.Tag.Count);
                Assert.IsTrue(Image.Tag.Contains(tag1));
                Assert.IsTrue(Image.Tag.Contains(tag2));
                Assert.IsTrue(Image.Tag.Contains(tag3));

                ImageService.UpdateImage(userId, Image.imgId, updated_tagsId);
                Assert.AreEqual(Image, FoundImage);
                Assert.AreEqual(0, Image.Tag.Count);
            }
        }

        [TestMethod]
        public void AddTag()
        {
            ImageService.AddTag("Pokemon");
            Tag tag = TagDao.FindByName("Pokemon");
            Assert.AreEqual("pokemon", tag.name);
        }

        [TestMethod]
        public void AddTagsToImage()
        {
            int startIndex = 0;
            int count = 2;

            var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

            catogoryDao.Create(category);

            ImageDTO ImageDTO1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
            ImageDTO ImageDTO2 = CreateImage(userId, "C:/Software/DataBase/Images/Pikachu", "Pokemon", "Otro", DateTime.Now, category.catId);

            TagDao.Create(tag1);
            TagDao.Create(tag2);
            TagDao.Create(tag3);

            IList<long> tagsId = new List<long>
            {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
            };

            IList<long> emptyTags = new List<long> { };

            Image Image1 = ImageService.PostImage(ImageDTO1, emptyTags);
            Image Image2 = ImageService.PostImage(ImageDTO2, emptyTags);

            ImagePageable FoundImages = ImageService.FindImagesByTagPageable(count, startIndex, tag1.tagId);
            Assert.AreEqual(0, FoundImages.ImageWithTagsDTO.Count);

            ImageService.AddTagsToImage(Image1.imgId, tagsId);

            FoundImages = ImageService.FindImagesByTagPageable(count, startIndex, tag1.tagId);
            Assert.AreEqual(1, FoundImages.ImageWithTagsDTO.Count);

        }


        [TestMethod]
        [ExpectedException(typeof(DuplicateInstanceException))]
        public void AddTagDuplicateInstance()
        {
            ImageService.AddTag("Digimon");
            ImageService.AddTag("Digimon");
        }

        [TestMethod]
        public void FindTags()
        {

            int startIndex = 0;
            int count = 2;

            var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

            catogoryDao.Create(category);

            ImageDTO ImageDTO1 = CreateImage(userId, "C:/Software/DataBase/Images/Bulbasaur", "Pokemon", "Otro", DateTime.Now, category.catId);
            ImageDTO ImageDTO2 = CreateImage(userId, "C:/Software/DataBase/Images/Pikachu", "Pokemon", "Otro", DateTime.Now, category.catId);

            TagDao.Create(tag1);
            TagDao.Create(tag2);
            TagDao.Create(tag3);

            IList<long> tagsId = new List<long>
            {
                    tag1.tagId,
                    tag2.tagId,
                    tag3.tagId
            };

            Image Image1 = ImageService.PostImage(ImageDTO1, tagsId);
            Image Image2 = ImageService.PostImage(ImageDTO2, tagsId);

            TagBlock tags = ImageService.FindTags(startIndex, count);
            Assert.AreEqual(2, tags.TagDto.Count);
        }

    }
}