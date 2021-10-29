using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    class CommentsDaoEntityFramework :
        GenericDaoEntityFramework<Comments, Int64>, ICommentsDao
    {
        public List<Comments> findByImage(long imgId)
        {

            DbSet<Comments> comments = Context.Set<Comments>();
            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from c in comments
                 from u in userProfiles
                 where c.usrId == u.usrId && c.imgId == imgId
                 orderby c.postDate
                 select c).ToList();

            return result;
        }
    }
}
