﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService
{
    class UserRelatedService : IUserRelatedService
    {

        public IImageDao ImageDao { private get; set; }

        public IFollowDao FollowDao { private get; set; }



        public void FollowUser(long usrId, long follow)
        {
            throw new NotImplementedException();
        }

    
        #region ViewUser
        public ImageBlock GetUserImages(long userProfileId, int startIndex, int count)
        {
            /*
           * Find count+1 accounts to determine if there exist more accounts above
           * the specified range.
           */
            List<Image> images =
                ImageDao.FindByDate(userProfileId, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            return new ImageBlock(images, existMoreImages);
        }

        public List<Follow> GetUserFollowers(long userProfileId)
        {
            if (!FollowDao.Exists(userProfileId))
            {
                throw new InstanceNotFoundException(userProfileId,
                    typeof(UserProfile).FullName);
            }

            /* Return count. */
            return FollowDao.GetFollowers(
                userProfileId);
        }

        public List<UserProfile> GetUserFollows(long userProfileId)
        {
            if (!FollowDao.Exists(userProfileId))
            {
                throw new InstanceNotFoundException(userProfileId,
                    typeof(UserProfile).FullName);
            }

            /* Return count. */
            return FollowDao.GetFollows(
                userProfileId);
        }

        #endregion
    }
}
