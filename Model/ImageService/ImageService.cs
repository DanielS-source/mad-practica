﻿using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using System.Runtime.Caching;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;

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


        [Transactional]
        public Image PostImage(Image image, IList<long> tagsIds)
        {

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

        public ImageBlock SearchFollowedImages(long usrId, int startIndex, int count)
        {

            List<Image> images = ImageDao.FindByFollowed(usrId, startIndex, count);

            bool existMoreImages = (images.Count == count);

            return new ImageBlock(images, existMoreImages);
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

        public void DeleteImage(long imageId)
        {
            Image image = ImageDao.Find(imageId);
            deleteImageFromPath(image.pathImg);
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

        private void deleteImageFromPath(String path) {

        }
    }
}
