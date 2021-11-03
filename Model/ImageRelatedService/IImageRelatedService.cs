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
    public interface IImageRelatedService
    {

        IImageDao ImageDao { set; }

        ICommentsDao CommentsDao { set; }

        ILikeDao LikeDao { set; }

    }
}
