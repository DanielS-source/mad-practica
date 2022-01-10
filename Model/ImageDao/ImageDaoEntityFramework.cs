using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService.Exceptions;
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

            DbSet<Image> images = Context.Set<Image>();
            if (category == null || category == "")
            {
                string[] searchTerms = keywords.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                var result =
                    (from i in images
                     where searchTerms.All(s => i.title.Contains(s)) | searchTerms.All(s => i.description.Contains(s))
                     orderby i.imgId
                     select i).Skip(startIndex * count).Take(count).ToList();
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
                     select i).Skip(startIndex * count).Take(count).ToList();
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
        public Image FindByUserWithTags(long userProfileId, string pathImg) //By UserProfileId and ImgPath
        {
            DbSet<Image> imageContext = Context.Set<Image>();

            Image image = imageContext.Include("Tag").SingleOrDefault(c => c.usrId.Equals(userProfileId) && (@c.pathImg).Equals(pathImg));

            if (image is null)
            {
                throw new InstanceNotFoundException(userProfileId, typeof(Image).FullName);
            }

            return image;
        }


        /// <exception cref="ArgumentException"/>
        /// <exception cref="PageableOutofRangeException"/>
        public IList<Image> FindByTag(int pageSize, int pageNumber, long tagId)
        {
                if (pageSize <= 0)
                {
                    throw new ArgumentException("Page size must be greater than zero");
                }

                DbSet<Image> imageContext = Context.Set<Image>();

                if (pageNumber < 0)
                {
                    throw new PageableOutofRangeException(pageNumber);
                }

                IList<Image> images = imageContext.Include("Tag").Where(i => i.Tag.Any(l => l.tagId.Equals(tagId))).OrderByDescending(c => c.dateImg).Skip(pageSize * pageNumber).Take(pageSize).ToList();

                if (pageNumber > 0 && images.Count().Equals(0))
                {
                    throw new PageableOutofRangeException(pageNumber);
                }

                return images;
        }

        public List<Image> FindByUser(long userId, int count)
        {
            DbSet<Image> imageContext = Context.Set<Image>();

            List<Image> images = imageContext.Include("LikedBy")
                .Where(i => i.usrId.Equals(userId))
                .OrderByDescending(i => i.dateImg)
                .Take(count)
                .ToList();

            return images;
        }
    }
}
