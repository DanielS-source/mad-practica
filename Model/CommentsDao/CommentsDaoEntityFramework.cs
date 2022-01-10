using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    public class CommentsDaoEntityFramework :
        GenericDaoEntityFramework<Comments, Int64>, ICommentsDao
    {
        [Inject]
        public IImageDao ImageDao { private get; set; }

        public List<Comments> findByImage(long imgId, int startIndex, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero");
            }

            if (startIndex < 0)
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }


            DbSet<Comments> comments = Context.Set<Comments>();

            var result =comments.Where(c => c.imgId == imgId)
                        .Include(c => c.UserProfile)
                        .OrderBy(c => c.postDate)
                        .Skip(startIndex * count)
                        .Take(count)
                        .ToList();
           
            if (startIndex > 0 && comments.Count().Equals(0))
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            return result;
        }
    }
}
