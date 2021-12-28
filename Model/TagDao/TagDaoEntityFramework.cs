using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework : GenericDaoEntityFramework<Tag, long>, ITagDao
    {
        /// <exception cref="InstanceNotFoundException"/>
        public Tag FindByName(string name)
        {
                DbSet<Tag> tagContext = Context.Set<Tag>();

                Tag tag = tagContext.SingleOrDefault(t => t.name.Equals(name));

                if (tag is null)
                {
                    throw new InstanceNotFoundException(name, typeof(Tag).FullName);
                }

                return tag;
        }

        /// <exception cref="ArgumentException"/>
        public IList<Tag> GetAllElementsPageable(int pageSize, int pageNumber)
        {
                if (pageSize <= 0)
                {
                    throw new ArgumentException("Page size must be greater than zero");
                }

                DbSet<Tag> tagContext = Context.Set<Tag>();

                if (pageNumber < 0)
                {
                    throw new ArgumentException("Page out of range" + pageNumber);
                }

                IList<Tag> tags = tagContext.OrderBy(t => t.tagId).Skip(pageSize * pageNumber).Take(pageSize).ToList();

                if (pageNumber > 0 && tags.Count().Equals(0))
                {
                throw new ArgumentException("Page out of range" + pageNumber);
            }

                return tags;
        }

        public IList<Tag> FindAllPosibleTagsinImages(int n)
        {
                DbSet<Tag> tagContext = Context.Set<Tag>();

                return tagContext.Include("Image").OrderByDescending(t => t.Image.Count()).Take(n).ToList();
        }

    }
}
