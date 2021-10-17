using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    interface IImageService
    {

        IImageDao ImageDao { set; }

        [Transactional]
        Image postImage(Image image);

        [Transactional]
        ImageBlock searchImages(string keywords, string category, int startIndex, int count);



    }
}
