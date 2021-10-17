using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    class CommentsDaoEntityFramework :
        GenericDaoEntityFramework<Comments, Int64>, ICommentsDao
    {
        public void AddComment(Comments comment)
        {

            try
            {
                Context.Set<Comments>().Add(comment);
                Context.SaveChanges();
            }
            catch (Exception e) 
            {
                Console.WriteLine(e); //Probar a lanzar InstaceNotFoundException
            }

        }

        public void DeleteComment(long comId)
        {
            throw new NotImplementedException();
        }

        public List<Comments> findByImage(long imgId)
        {

            DbSet<Comments> comments = Context.Set<Comments>();

            var result =
                (from c in comments
                 where c.imgId == imgId
                 orderby c.PostDate
                 select c).ToList();

            return result;
        }

        public void ModifyComment(long comId, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
