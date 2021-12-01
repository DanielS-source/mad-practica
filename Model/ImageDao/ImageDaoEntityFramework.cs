using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Linq.SqlClient;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public class ImageDaoEntityFramework :
        GenericDaoEntityFramework<Image, Int64>, IImageDao
    {

        public List<Image> FindByKeywordsAndCategory(string keywords, string category,
            int startIndex, int count)
        {

            //String query = "SELECT VALUE a FROM PhotgramEntitiesContainer.Image AS i "
            //    + "WHERE i.title = @keywords OR i.description = @keywords"
            //    + "ORDER BY i.dateImg";

            //ObjectParameter param = new ObjectParameter("keywords", keywords);

            //List<Image> result =
            //    this.Context.CreateQuery<Image>(query, param).Skip(startIndex).
            //            Take(count).ToList();
            //ObjectQuery<Image> adQuery = ((IObjectContextAdapter)Context).ObjectContext.CreateQuery<Image>(query, param) );
            //return result;

            DbSet<Image> images = Context.Set<Image>();
            if (category == null)
            {
                string[] searchTerms = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                var result =
                    (from i in images
                     where searchTerms.All(s => i.title.Contains(s)) | searchTerms.All(s => i.description.Contains(s))
                     orderby i.imgId
                     select i).Skip(startIndex).Take(count).ToList();
                return result;
            }
            else
            {

                DbSet<Category> categories = Context.Set<Category>();
                string[] searchTerms = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                var result =
                    (from i in images
                     where (
                        i.catId == (from c in categories where c.name == category select c.catId).FirstOrDefault())
                        & (searchTerms.All(s => i.title.Contains(s)) | searchTerms.All(s => i.description.Contains(s)))
                     orderby i.imgId
                     select i).Skip(startIndex).Take(count).ToList();
                return result;
            }

        }

        public List<Image> FindByDate(long userProfileId, int startIndex, int count)
        {
            //Option 1: Using Linq.

            DbSet<Image> images = Context.Set<Image>();

            List<Image> result =
                (from image in images
                 where image.usrId == userProfileId
                 orderby image.dateImg
                 select image).Skip(startIndex).Take(count).ToList();

            return result;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public Image FindByUserWithTags(long userProfileId)
        {
            DbSet<Image> imageContext = Context.Set<Image>();

            Image image = imageContext.Include("Tag").SingleOrDefault(c => c.usrId.Equals(userProfileId));

            if (image is null)
            {
                throw new InstanceNotFoundException(userProfileId, typeof(Image).FullName);
            }

            return image;
        }

        /// <exception cref="ArgumentException"/>
        public List<Image> FindByTag(long tagId, int startIndex, int count)
        {

            if (count <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero");
            }

            DbSet<Image> imageContext = Context.Set<Image>();

            if (startIndex < 0)
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }


            List<Image> images = imageContext.Include("Tag").Where(c => c.Tag.Any(t => t.tagId.Equals(tagId))).OrderByDescending(c => c.dateImg).Skip(count * startIndex).Take(count).ToList();

            if (startIndex > 0 && images.Count().Equals(0))
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            return images;

        }

    }
}
