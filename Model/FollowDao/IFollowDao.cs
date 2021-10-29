using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.FollowDao
{
    interface IFollowDao : IGenericDao<Follow, Int64>
    {
        /// <summary>
        /// Returns a list of followers search by userId
        /// </summary>
        /// <param name="userProfileId">The user identifier.</param>
        /// <returns>
        /// The collection of <c>Follow</c> objects.
        /// </returns>

        /*SQL: Tomar todos los valores en Follow donde usrId=idUser && Tomamos followerId*/
        List<Follow>GetFollowers(long userProfileId);

        /// <summary>
        /// Returns a list of all users who userId follow
        /// </summary>
        /// <param name="userProfileId">The user identifier.</param>
        /// <returns>
        /// The collection of <c>Follow</c> objects.
        /// </returns>
        /*SQL: Tomar todos los valores en Follow donde followerId=idUser && Tomamos usrId*/
        List<UserProfile> GetFollows(long userProfileId);


    }
}
