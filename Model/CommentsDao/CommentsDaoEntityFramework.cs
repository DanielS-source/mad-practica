using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    public class CommentsDaoEntityFramework :
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
            Comments commentToRemove = default(Comments);
            try
            {
                commentToRemove = this.find(comId);
                Context.Set<Comments>().Remove(commentToRemove);
                Context.SaveChanges();
            }
            catch(InvalidOperationException)
            { 
                    
            }
        }

        public Comments find(long comId) 
        {
            Comments result = Context.Set<Comments>().Find(comId);

            if (result == null)
                return null;
            //devolver excepcion InstanceNotFound
            else
                return result;
        }

        public List<Comments> findByImage(long imgId)
        {

            DbSet<Comments> comments = Context.Set<Comments>();

            var result =
                (from c in comments
                 where c.imgId == imgId
                 orderby c.postDate
                 select c).ToList();

            return result;
        }

        public void ModifyComment(long comId, string comment)
        {
            throw new NotImplementedException();
        }
    }
}
