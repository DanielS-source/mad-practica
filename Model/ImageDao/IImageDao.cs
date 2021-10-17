using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {

        List<Image> FindByKeywordsAndCategory(string keywords, string category, int startIndex, int count);

    }

}