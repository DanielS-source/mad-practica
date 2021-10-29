using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService
{
    public interface IUserRelatedService
    {

        void FollowUser(long usrId, long follow);

        #region ViewUser
        [Transactional]
        ImageBlock GetUserImages(long userProfileId, int startIndex, int count);

        List<Follow> GetUserFollowers(long userProfileId);

        List<UserProfile> GetUserFollows(long userProfileId);
        #endregion
    }
}
