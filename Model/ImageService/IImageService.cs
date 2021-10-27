using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {

        IImageDao ImageDao { set; }

        [Transactional]
        Image PostImage(Image image);

        [Transactional]
        ImageBlock SearchImages(string keywords, string category, int startIndex, int count);

        [Transactional]
        ImageBlock SearchFollowedImages(long usrId, int startIndex, int count);


    }
}
