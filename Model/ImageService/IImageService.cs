using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService.Exceptions;
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
        Image PostImage(ImageDTO imageDTO, IList<long> tagsIds);

        [Transactional]
        SearchImageBlock SearchImages(string keywords, string category, int startIndex, int count);

        /// <exception cref="ArgumentException" />
        /// <exception cref="PageableOutofRangeException" />
        ImagePageable FindImagesByTagPageable(int pageSize, int pageNumber, long tagId);

        //[Transactional]
        //SearchImageBlock FindImagesByTag(long tagId, int startIndex, int count);

        [Transactional]
        void DeleteImage(long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        void UpdateImage(long usrId, long imgId, IList<long> tagsId);

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long AddTag(string name);

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        void AddTagsToImage(long imgId, IList<long> tagsId);

        /// <exception cref="ArgumentException" />
        /// <exception cref="PageableOutofRangeException"/>
        [Transactional]
        TagBlock FindTags(int pageSize, int pageNumber);

        IList<TagDTO> FindTagsOnImages(int n);

        [Transactional]
        long AddComment(long userId, long imgId, string message);

        [Transactional]
        void EditComment(long comId, String text);

        [Transactional]
        CommentsBlock GetImageRelatedComments(long imgId, int startIndex, int count);

        [Transactional]
        void LikeImage(long usrId, long imgId);

        [Transactional]
        List<Category> GetAllCategories();
    }

}
