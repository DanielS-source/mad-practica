using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.LikeDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageRelatedService
{
    interface IImageRelatedService
    {

        IImageDao ImageDao { set; }

        ICommentsDao CommentsDao { set; }

        ILikeDao LikeDao { set; }

        [Transactional]
        Likes LikeImage(Likes like);

        #region Comments
        [Transactional]
        Comments AddComment(Comments comment);

        [Transactional]
        void EditComment(long comId, String text);

        [Transactional]
        List<Comments> GetImageRelatedComments(long imgId);

        #endregion

    }

}
