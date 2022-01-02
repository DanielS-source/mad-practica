using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework : GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        public Category FindByName(string categoryName)
        {
            DbSet<Category> categories = Context.Set<Category>();

            Category result =
                (from cat in categories
                 where cat.name == categoryName
                 select cat).First();

            return result;
        }
    }
}
