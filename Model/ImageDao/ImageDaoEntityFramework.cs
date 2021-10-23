using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            var result =
                (from i in images
                 where SqlMethods.Like(i.title,keywords) | SqlMethods.Like(i.description, keywords)
                 orderby i.imgId
                 select i).Skip(startIndex).Take(count).ToList();

            return result;

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
    }

}
