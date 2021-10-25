using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService
{
    class UserRelatedService : IUserRelatedService
    {
        public UserRelatedService() { }

        [Inject]
        public IFollowDao FollowDao { private get; set; }

        [Transactional]
        public void FollowUser(long usrId, long follower)
        {
            Follow follow = new Follow();
            follow.usrId = follower;
            follow.followerId = usrId;

            FollowDao.Create(follow);
        }
    }
}
