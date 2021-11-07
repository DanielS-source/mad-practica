using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {
        IImageDao ImageDao { set; }
        ITagDao TagDao { set; }

        [Transactional]
        Image PostImage(Image image, IList<long> tagsIds);

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

        /// <exception cref="InstanceNotFoundException" />
        [Transactional]
        void RemoveImage(long imgId);

        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        long AddTag(string name);

        /// <exception cref="ArgumentException" />
        [Transactional]
        TagBlock FindTags(int startIndex, int count);
    }
}
