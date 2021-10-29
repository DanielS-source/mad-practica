using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    public interface ICommentsDao : IGenericDao<Comments, Int64>
    {
        /// <summary>
        /// Finds all the comments related to a given image 
        /// </summary>
        /// <param name="imgId">the image identifier</param>
        /// <returns>the list of comments related to the given image</returns>
        List<Comments> findByImage(long imgId);
    }
}
