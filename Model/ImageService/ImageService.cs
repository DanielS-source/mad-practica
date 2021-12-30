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

        public ImageService()
        {
        }

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
            image.pathImg = "C:\\Software\\DataBase\\Images\\" + image.usrId + image.dateImg;
            image.catId = CategoryDao.FindByName(imageDTO.category).catId;

            System.Drawing.Image img = System.Drawing.Image.FromStream(imageDTO.file);
            img.Save(image.pathImg, ImageFormat.Jpeg);

            foreach (long tagId in tagsIds)
            {
                Tag tag = TagDao.Find(tagId);

                image.Tag.Add(tag);
            }

            ImageDao.Create(image);

            return image;
        }

        [Transactional]
        public ImageBlock SearchImages(string keywords, string category, int startIndex, int count)
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

            return new ImageBlock(images, existMoreImages);
        }

        /// <exception cref="ArgumentException"/>
        /// <exception cref="PageableOutofRangeException"/>
        public ImagePageable FindImagesByTagPageable(int pageSize, int pageNumber, long tagId)
        {
            IList<ImageWithTagsDto> imagesDTO = new List<ImageWithTagsDto>();

            foreach (Image image in ImageDao.FindByTag(tagId, pageNumber, pageSize))
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

                imagesDTO.Add(new ImageWithTagsDto(
                    image,
                    userProfile.loginName,
                    tagsDTO
                ));
            }

            try
            {
                ImageDao.FindByTag(tagId, pageNumber + 1, pageSize);

                return new ImagePageable(imagesDTO, !pageNumber.Equals(0), true);
            }
            catch (PageableOutofRangeException)
            {
                return new ImagePageable(imagesDTO, !pageNumber.Equals(0), false);
            }
        }


        /// <exception cref="ArgumentException"/>
        public ImageBlock FindImagesByTag(long tagId, int startIndex, int count)
        {
            List<Image> images = ImageDao.FindByTag(tagId, startIndex, count);

            bool existMoreImages = (images.Count == count);

            return new ImageBlock(images, existMoreImages);
        }

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
        public TagBlock FindTags(int startIndex, int count)
        {
            IList<TagBlock> tags = new List<TagBlock>();
            foreach (Tag tag in TagDao.GetAllElementsPageable(count, startIndex))
            {
                tags.Add(new TagBlock(
                    tag.tagId,
                    tag.name,
                    tag.uses
                ));
            }

            TagDao.GetAllElementsPageable(count, startIndex + 1);

            return new TagBlock(tags, !startIndex.Equals(0), true);

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

        public List<Category> GetAllCategories()
        {
            Console.Write("patata");
            return CategoryDao.GetAllElements();
        }
    }
}
