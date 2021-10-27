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
        public ImageRelatedService()
        {
        }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ICommentsDao CommentsDao { private get; set; }

        [Inject]
        public ILikeDao LikeDao { private get; set; }


    }
}
