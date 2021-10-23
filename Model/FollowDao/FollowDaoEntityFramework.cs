using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.FollowDao
{
    public class FollowDaoEntityFramework :
    GenericDaoEntityFramework<Follow, Int64>, IFollowDao
    {
        //Follow
        //UserProfile
        public List<Follow> GetFollowers(long userProfileId, int startIndex, int count)
        {
            DbSet<UserProfile> followers = Context.Set<UserProfile>();

            List<Follow> result =
                (from fw in followers
                 where fw.usrId == userProfileId
                 select fw.Follow).First().ToList<Follow>();

            return result;
        }

        public List<UserProfile> GetFollows(long userProfileId, int startIndex, int count)
        {
            DbSet<Follow> follow = Context.Set<Follow>();

            List<UserProfile> result =
                (from fw in follow
                 where fw.followerId == userProfileId
                 select fw.UserProfile1).ToList<UserProfile>();

            return result;
        }
    }

}
