using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.LikeDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageRelatedService
{
    public class ImageRelatedService : IImageRelatedService
    {
        public ImageRelatedService() { }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ICommentsDao CommentsDao { private get; set; }

        [Inject]
        public ILikeDao LikeDao { private get; set; }

        [Transactional]
        public Likes LikeImage(Likes like) 
        {
            LikeDao.Create(like);
            return like;
        }

        #region Comments
        [Transactional]
        public Comments AddComment(Comments comment)
        {
            CommentsDao.Create(comment);
            return comment;

        }

        [Transactional]
        public void EditComment(long comId, String text)
        {
            Comments comment = CommentsDao.Find(comId);

            if (comment != null)
            {
                comment.message = text;
            }
        }

        [Transactional]
        public List<Comments> GetImageRelatedComments(long imgId)
        {
            return CommentsDao.findByImage(imgId);
        }
        #endregion


    }
}
