using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService.Exceptions;
using System.Drawing.Imaging;
using System.IO;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        private List<string> keys = new List<string>();

        public ImageService() {}

        [Inject]
        public IImageDao ImageDao { private get; set; }

       [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public ICommentsDao CommentsDao { private get; set; }

        [Inject]
        public IUserProfileDao UserProfileDao{ private get; set; }

        [Inject]
        public ICategoryDao CategoryDao{ private get; set; }

        [Transactional]
        public Image PostImage(ImageDTO imageDTO, 
            IList<long> tagsIds)
        {

            Image image = AdaptToImage(imageDTO);
            image.dateImg = DateTime.Now;
            image.pathImg = "C:\\Software\\DataBase\\Images\\" + image.usrId + image.dateImg.Ticks;
            image.catId = CategoryDao.FindByName(imageDTO.category).catId;
            //Check whether Directory (Images) exists.
            if (!Directory.Exists("C:\\Software\\DataBase\\Images\\"))
            {
                //If Directory (Images) does not exists. Create it.
                Directory.CreateDirectory("C:\\Software\\DataBase\\Images\\");
            }
            File.WriteAllBytes(image.pathImg, imageDTO.file);

            foreach (long tagId in tagsIds)
            {
                Tag tag = TagDao.Find(tagId);

                image.Tag.Add(tag);
            }

            ImageDao.Create(image);

            return image;
        }

        [Transactional]
        public SearchImageBlock SearchImages(string keywords, string category, int startIndex, int count)
        {
            List<Image> images = new List<Image>();

            //Checks if its in the cache
            if (ImageCache.Exists(keywords + category)) {
                images = ImageCache.Get<List<Image>>(keywords + category);
            }
            else
            {
                images = ImageDao.FindByKeywordsAndCategory(keywords, category, startIndex, count);

                //Adds the results to the cache
                ImageCache.Add(keywords + category, images);
            }

            bool existMoreImages = (images.Count == count);

            return new SearchImageBlock(AdaptToSearchImageDTOs(images), existMoreImages);
        }

        public SearchImageDTO GetImageById(long imgId)
        {
            return AdaptToSearchImageDTO(ImageDao.Find(imgId));
        }

        /// <exception cref="ArgumentException"/>
        /// <exception cref="PageableOutofRangeException"/>
        public ImagePageable FindImagesByTagPageable(int pageSize, int pageNumber, long tagId)
        {
            IList<ImageWithTagsDto> imagesDTO = new List<ImageWithTagsDto>();
            string category;
            string username;
            byte[] img;
            List<Comments> comments;

            foreach (Image image in ImageDao.FindByTag(pageSize, pageNumber, tagId))
            {
                UserProfile userProfile = UserProfileDao.Find(image.usrId);

                IList<TagDTO> tagsDTO = new List<TagDTO>();
                foreach (Tag tag in image.Tag)
                {
                    tagsDTO.Add(new TagDTO(
                        tag.tagId,
                        tag.name
                    ));
                }

                category = CategoryDao.Find(image.catId).name;
                username = UserProfileDao.Find(image.usrId).loginName;
                img = File.ReadAllBytes(image.pathImg);
                comments = null;

                imagesDTO.Add(new ImageWithTagsDto(
                    image,
                    tagsDTO,
                    userProfile.loginName,
                    category,
                    img,
                    comments
                ));
            }

            try
            {
                ImageDao.FindByTag(pageSize, pageNumber + 1, tagId);

                return new ImagePageable(imagesDTO, !pageNumber.Equals(0), true);
            }
            catch (PageableOutofRangeException)
            {
                return new ImagePageable(imagesDTO, !pageNumber.Equals(0), false);
            }
        }


        /// <exception cref="ArgumentException"/>
        /*public SearchImageBlock FindImagesByTag(long tagId, int startIndex, int count)
        {
            List<Image> images = ImageDao.FindByTag(tagId, startIndex, count);

            bool existMoreImages = (images.Count == count);

            return new SearchImageBlock(AdaptToSearchImageDTOs(images), existMoreImages);
        }*/

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateImage(long usrId, long imgId, IList<long> tagsId)
        {

            Image image = ImageDao.FindByUserWithTags(usrId);

            IList<Tag> tags = new List<Tag>();

            foreach (long tagId in tagsId)
            {
                Tag tag = TagDao.Find(tagId);
                tags.Add(tag);
            }

            image.Tag.Clear();

            foreach (Tag tag in tags)
            {
                image.Tag.Add(tag);
            }

            ImageDao.Update(image);
        }

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long AddTag(string name)
        {
            name = name.ToLower();

            try
            {
                TagDao.FindByName(name);

                throw new DuplicateInstanceException(name, typeof(Tag).FullName);
            }
            catch (InstanceNotFoundException)
            {
                Tag tag = new Tag()
                {
                    name = name.ToLower()
                };

                TagDao.Create(tag);

                return tag.tagId;
            }
        }

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public void AddTagsToImage(long imgId, IList<long> tagsId)
        {
            Image image = ImageDao.Find(imgId);

            IList<Tag> tags = new List<Tag>();

            foreach (long tagId in tagsId)
            {
                Tag tag = TagDao.Find(tagId);
                tags.Add(tag);
            }

            image.Tag.Clear();

            foreach (Tag tag in tags)
            {
                image.Tag.Add(tag);
            }

            ImageDao.Update(image);
        }

        /// <exception cref="ArgumentException"/>
        /// <exception cref="PageableOutofRangeException"/>
        public TagBlock FindTags(int pageSize, int pageNumber)
        {
            IList<TagDTO> tagDto = new List<TagDTO>();
            foreach (Tag tag in TagDao.GetAllElementsPageable(pageSize, pageNumber))
            {
                tagDto.Add(new TagDTO(
                    tag.tagId,
                    tag.name,
                    tag.Image.Count()
                ));
            }

            try
            {
                TagDao.GetAllElementsPageable(pageSize, pageNumber + 1);

                return new TagBlock(tagDto, !pageNumber.Equals(0), true);
            }
            catch (PageableOutofRangeException)
            {
                return new TagBlock(tagDto, !pageNumber.Equals(0), false);
            }

        }

        public IList<TagDTO> FindTagsOnImages(int n)
        {
            IList<TagDTO> tagDto = new List<TagDTO>();
            foreach (Tag tag in TagDao.FindAllPosibleTagsinImages(n))
            {
                tagDto.Add(new TagDTO(
                    tag.tagId,
                    tag.name,
                    tag.Image.Count()
                ));
            }

            return tagDto;

        }


        public void DeleteImage(long imageId)
        {
            Image image = ImageDao.Find(imageId);
            DeleteImageFromPath(image.pathImg);
            ImageDao.Remove(imageId);
        }

        [Transactional]
        public void LikeImage(long userId, long imgId)
        {
            Image image = ImageDao.Find(imgId);
            UserProfile user = UserProfileDao.Find(userId);
            image.LikedBy.Add(user);
            ImageDao.Update(image);
            return;
        }

        public long AddComment(long userId, long imgId, string message) 
        {
            Comments comment = new Comments()
            {
                imgId = imgId,
                usrId = userId,
                message = message,
                postDate = DateTime.Now
            };

            CommentsDao.Create(comment);

            return comment.comId;
        }

        public void EditComment(long comId, String text)
        {
            Comments comment = CommentsDao.Find(comId);

            if (comment != null)
            {
                comment.message = text;
            }
        }

        public CommentsBlock GetImageRelatedComments(long imgId, int startIndex, int count)
        {
            List<CommentsWithUsernameDto> commentsWithUsername = new List<CommentsWithUsernameDto>();
            foreach(Comments c in CommentsDao.findByImage(imgId, startIndex, count))
            {
                commentsWithUsername.Add(new CommentsWithUsernameDto(
                        c.comId,
                        c.imgId,
                        c.usrId,
                        c.message,
                        c.postDate,
                        c.UserProfile.loginName
                    ));
            }

            bool existMore = commentsWithUsername.Count == count;

            return new CommentsBlock(commentsWithUsername, existMore);
        }

        private void DeleteImageFromPath(String path) {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private ImageDTO ToImageDTO(Image image)
        {
            return new ImageDTO(image, CategoryDao.Find(image.catId).name);
        }

        private List<ImageDTO> ToImageDTOs(List<Image> images)
        {
            List<ImageDTO> imageDTOs = new List<ImageDTO>();
            foreach(Image image in images)
            {
                imageDTOs.Add(ToImageDTO(image));
            }

            return imageDTOs;
        }

        private Image AdaptToImage(ImageDTO imageDTO)
        {
            return new Image
            {
                usrId = imageDTO.usrId,
                title = imageDTO.title,
                description = imageDTO.description,
                f = imageDTO.f,
                t = imageDTO.t,
                ISO = imageDTO.ISO,
                wb = imageDTO.wb
            };
        }

        private List<SearchImageDTO> AdaptToSearchImageDTOs(List<Image> images)
        {
            List<SearchImageDTO> searchImages = new List<SearchImageDTO>();
            foreach (Image image in images)
            {
                searchImages.Add(AdaptToSearchImageDTO(image));
            }
            return searchImages;
        }

        private SearchImageDTO AdaptToSearchImageDTO(Image image)
        {
            string category = CategoryDao.Find(image.catId).name;
            string username = UserProfileDao.Find(image.usrId).loginName;
            byte[] img = File.ReadAllBytes(image.pathImg);
            List<Comments> comments = null;//CommentsDao.findByImage(image.imgId, 0, 2);
            SearchImageDTO search = new SearchImageDTO(image, username, category, img, comments);
            string imreBase64Data = Convert.ToBase64String(img);
            search.imageSrc = string.Format("data:image/png;base64,{0}", imreBase64Data);
            return search;
        }

        public List<Category> GetAllCategories()
        {
            return CategoryDao.GetAllElements();
        }

        public SearchImageBlock FinByUser(long userId, int count) 
        {
            List<Image> images = ImageDao.FindByUser(userId, count);

            bool existMore = (images.Count == count);

            return new SearchImageBlock(AdaptToSearchImageDTOs(images), existMore);
        }
    }
}
