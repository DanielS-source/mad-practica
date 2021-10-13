using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {

        List<Image> FindByImageId(long imgId, int startIndex, int count);



    }

}