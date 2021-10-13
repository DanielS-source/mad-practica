using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public class ImageDaoEntityFramework :
        GenericDaoEntityFramework<Image, Int64>, IImageDao
    {
        List<Image> IImageDao.FindByImageId(long imgId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
    }

}