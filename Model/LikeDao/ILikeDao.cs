using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.LikeDao
{
    interface ILikeDao : IGenericDao<Likes, Int64>
    {

        List<Likes> FindByImageId(long imgId, int startIndex, int count);



    }
}
