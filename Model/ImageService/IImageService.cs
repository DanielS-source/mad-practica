using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {
        IImageDao ImageDao { set; }
        ITagDao TagDao { set; }

        ICommentsDao CommentsDao { set; }

        [Transactional]
        Image PostImage(long usrId, string pathImg, string title, string description,
            DateTime dateImg, long catId, string f, string t, string ISO, string wb,
            IList<long> tagsIds);

        [Transactional]
        ImageBlock SearchImages(string keywords, string category, int startIndex, int count);

        [Transactional]
        ImageBlock SearchFollowedImages(long usrId, int startIndex, int count);


        [Transactional]
        ImageBlock FindImagesByTag(long tagId, int startIndex, int count);

        [Transactional]
        void DeleteImage(long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateImage(long usrId, long imgId, IList<long> tagsId);

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long AddTag(string name);

        /// <exception cref="ArgumentException" />
        [Transactional]
        TagBlock FindTags(int startIndex, int count);

        [Transactional]
        long AddComment(long userId, long imgId, string message);

        [Transactional]
        void EditComment(long comId, String text);

        [Transactional]
        CommentsBlock GetImageRelatedComments(long imgId, int startIndex, int count);

        [Transactional]
        void LikeImage(long usrId, long imgId);
    }

}
