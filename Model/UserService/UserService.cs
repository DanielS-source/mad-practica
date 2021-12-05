using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        // void ChangePassword(long userProfileId, String oldClearPassword, String newClearPassword);
        public void ChangePassword(long userProfileId, string oldClearPassword, string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);

        }

        // UserProfileDetails FindUserProfileDetails(long userProfileId);
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language);

            return userProfileDetails;

        }

        // LoginResult Login(String loginName, String password, Boolean passwordIsEncrypted);
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile =
                  UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.usrId, userProfile.firstName,
                storedPassword, userProfile.language);

        }

        // long RegisterUser(String loginName, String clearPassword, UserProfileDetails userProfileDetails);
        public long RegisterUser(string loginName, string clearPassword,UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.language = userProfileDetails.Language;

                UserProfileDao.Create(userProfile);

                return userProfile.usrId;
            }

        }

        // void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails);
        public void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.language = userProfileDetails.Language;
            UserProfileDao.Update(userProfile);

        }

        // bool UserExists(string loginName);
        public bool UserExists(string loginName)
        {

            try
            {
                UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public void FollowUser(long usrId, long followedUsrId) 
        {
            UserProfile user = UserProfileDao.Find(usrId);
            UserProfile followedUser = UserProfileDao.Find(followedUsrId);
            user.Follows.Add(followedUser);
            return;
        }

        public UserBlock GetFollowers(long userId, int startIndex, int count)
        {
            if (!UserProfileDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);
            }

            List<UserProfile> followersFromDb = UserProfileDao.GetFollowers(userId, startIndex, count);
            List<UserProfileDetails> followers = new List<UserProfileDetails>();

            foreach (UserProfile u in followersFromDb) 
            {
                UserProfileDetails userProfileDetails =
                new UserProfileDetails(u.firstName,
                    u.lastName, u.email,
                    u.language);

                followers.Add(userProfileDetails);
            }

            bool existMore = followers.Count == count;

            return new UserBlock(followers, existMore);
        }

        public UserBlock GetFollowed(long userId, int startIndex, int count)
        {
            if (!UserProfileDao.Exists(userId))
            {
                throw new InstanceNotFoundException(userId,
                    typeof(UserProfile).FullName);
            }

            List<UserProfile> followedFromDb = UserProfileDao.GetFollowed(userId, startIndex, count);
            List<UserProfileDetails> followed = new List<UserProfileDetails>();

            foreach (UserProfile u in followedFromDb)
            {
                UserProfileDetails userProfileDetails =
                new UserProfileDetails(u.firstName,
                    u.lastName, u.email,
                    u.language);

                followed.Add(userProfileDetails);
            }

            bool existMore = followed.Count == count;

            return new UserBlock(followed, existMore);
        }

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
    }
}
