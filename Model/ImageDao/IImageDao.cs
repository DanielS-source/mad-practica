using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageDao
{

    public interface IImageDao : IGenericDao<Image, Int64>
    {

        List<Image> FindByKeywordsAndCategory(string keywords, string category, int startIndex, int count);

        /// <summary>
        /// Returns a list of user images order by upload date
        /// </summary>
        /// <param name="userProfileId">The user identifier.</param>
        /// <param name="startIndex">The index of the first object to return
        /// (starting in 0).</param>
        /// <param name="count">The maximum number of objects to return.
        /// </param>
        /// <returns>
        /// The collection of <c>AccountOperation</c> objects.
        /// </returns>
        List<Image> FindByDate(long userProfileId, int startIndex, int count);

        List<Image> FindByFollowed(long usrId, int startIndex, int count);

    }

}