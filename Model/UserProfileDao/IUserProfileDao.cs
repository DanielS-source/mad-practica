using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, long>
    {
        /// <summary>
        /// Finds a UserProfile by loginName
        /// </summary>
        /// <param name="loginName">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(string loginName);

        List<UserProfile> GetFollowers(long userId, int startIndex, int count);

        List<UserProfile> GetFollowed(long userId, int startIndex, int count);

        List<UserProfile> GetAllFollowed(long userId);
    }
}
