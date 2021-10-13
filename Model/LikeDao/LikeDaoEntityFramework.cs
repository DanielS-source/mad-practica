using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.LikeDao
{
    public class LikeDaoEntityFramework :
        GenericDaoEntityFramework<Likes, Int64>, ILikeDao
    {
        List<Likes> ILikeDao.FindByImageId(long imgId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
    }
}
