using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService
{
    interface IUserRelatedService
    {

        void FollowUser(long usrId, long follow);

    }
}
