using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, long>
    {
        /// <exception cref="InstanceNotFoundException"/>
        Tag FindByName(string name);

        IList<Tag> FindAllPosibleTagsinImages(int n);

        /// <exception cref="ArgumentException"/>
        /// <exception cref="PageableOutofRangeException"/>
        IList<Tag> GetAllElementsPageable(int pageSize, int pageNumber);

    }
}
