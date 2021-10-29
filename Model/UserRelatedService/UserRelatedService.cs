using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.FollowDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserRelatedService
{
    public class UserRelatedService : IUserRelatedService
    {
        public UserRelatedService() { }

        [Inject]
        public IFollowDao FollowDao { private get; set; }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public ICommentsDao CommentsDao { private get; set; }
        
        [Transactional]
        public void FollowUser(long usrId, long follower)
        {
            Follow follow = new Follow();
            follow.usrId = follower;
            follow.followerId = usrId;

            FollowDao.Create(follow);
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


        #region Comments
        [Transactional]
        public Comments AddComment(Comments comment) 
        {
            CommentsDao.Create(comment);
            return comment;

        }

        [Transactional]
        public void EditComment(long comId, String text) 
        {
            Comments comment = CommentsDao.Find(comId);

            if (comment != null) 
            {
                comment.message = text;
            }
        }

        [Transactional]
        public List<Comments> GetImageRelatedComments(long imgId) 
        {
            return CommentsDao.findByImage(imgId);
        }

        #endregion
    }
}
