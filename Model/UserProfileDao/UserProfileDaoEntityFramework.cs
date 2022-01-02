using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework : 
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {

        /// <summary>
        /// Finds a UserProfile by his loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        /// #Using Linq.
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        public List<UserProfile> GetFollowers(long userId, int startIndex, int count)
        {

            if (count <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero");
            }

            if (startIndex < 0)
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            DbSet<UserProfile> userProfileContext = Context.Set<UserProfile>();

            List<UserProfile> followers = userProfileContext.Include("Followers").
                Where(u => u.usrId.Equals(userId)).
                OrderBy(f => f.usrId).
                Skip(count * startIndex).
                Take(count).
                ToList();

            if (startIndex > 0 && followers.Count().Equals(0))
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            return followers[0].Followers.
                Skip(count * startIndex).
                Take(count).
                ToList();
        }

        public List<UserProfile> GetFollowed(long userId, int startIndex, int count)
        {

            if (count <= 0)
            {
                throw new ArgumentException("Page size must be greater than zero");
            }

            if (startIndex < 0)
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            DbSet<UserProfile> userProfileContext = Context.Set<UserProfile>();

            List<UserProfile> followed = userProfileContext.Include("Follows").
                Where(u => u.usrId.Equals(userId)).
                OrderBy(u => u.usrId).
                Skip(count * startIndex).
                Take(count).
                ToList();

            if (startIndex > 0 && followed.Count().Equals(0))
            {
                throw new ArgumentException("Page out of range" + startIndex);
            }

            return followed[0].Follows.
                Skip(count * startIndex).
                Take(count).
                ToList();
        }

        public List<UserProfile> GetAllFollowed(long userId)
        {
            DbSet<UserProfile> userProfileContext = Context.Set<UserProfile>();

            List<UserProfile> followed = userProfileContext.Include("Follows").
                Where(u => u.usrId.Equals(userId)).
                OrderBy(f => f.usrId).
                ToList();

            return followed;
        }
    }

}
