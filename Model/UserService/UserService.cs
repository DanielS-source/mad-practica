using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
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

        /// <exception cref="InputValidationException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangeCulture(long userProfileId, string language, string country)
        {
            if (!PropertyValidator.IsValidCulture(language, country))
            {
                throw new InputValidationException("Invalid culture value (it must follow the ISO-639 for the language code and the ISO-3166 for the country code): " + language + "-" + country);
            }

            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            userProfile.language = language;
            userProfile.country = country;

            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="InputValidationException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangeEmail(long userProfileId, string email)
        {
            if (!PropertyValidator.IsValidEmail(email))
            {
                throw new InputValidationException("Invalid email, it must be between 1 and 40 characters and contain an '@' before a '.' (Email=" + email + ")");
            }

            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            userProfile.email = email;

            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="InputValidationException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangeFirstNameLastName(long userProfileId, string firstName, string lastName)
        {
            if (!PropertyValidator.IsValidFirstName(firstName))
            {
                throw new InputValidationException("Invalid first name, it must be between 1 and 40 characters (FirstName=" + firstName + ")");
            }
            if (!PropertyValidator.IsValidLastName(lastName))
            {
                throw new InputValidationException("Invalid last name, it must be between 1 and 40 characters (LastName=" + lastName + ")");
            }

            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            userProfile.firstName = firstName;
            userProfile.lastName = lastName;

            UserProfileDao.Update(userProfile);
        }


        // UserProfileDetails FindUserProfileDetails(long userProfileId);
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.language, userProfile.country);
            Console.Write(userProfileDetails.Email);
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
                storedPassword, userProfile.language, userProfile.country);

        }

        // long RegisterUser(String loginName, String clearPassword, UserProfileDetails userProfileDetails);
        /// <exception cref="InputValidationException"/>
        /// <exception cref="DuplicateInstanceException"/>
        public long RegisterUser(string loginName, string clearPassword, UserProfileDetails userProfileDetails)
        {

            ValidateUserProfile(userProfileDetails);

            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName, typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                if (userProfileDetails.Language is null || userProfileDetails.Country is null)
                {
                    userProfile.loginName = loginName;
                    userProfile.enPassword = encryptedPassword;
                    userProfile.firstName = userProfileDetails.FirstName;
                    userProfile.lastName = userProfileDetails.Lastname;
                    userProfile.email = userProfileDetails.Email;
                }
                else
                {
                    userProfile.loginName = loginName;
                    userProfile.enPassword = encryptedPassword;
                    userProfile.firstName = userProfileDetails.FirstName;
                    userProfile.lastName = userProfileDetails.Lastname;
                    userProfile.email = userProfileDetails.Email;
                    userProfile.language = userProfileDetails.Language;
                    userProfile.country = userProfileDetails.Country;
                }

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
            userProfile.country = userProfileDetails.Country;
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
                    u.language, u.country);

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
                    u.language, u.country);

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

        /// <exception cref="InputValidationException"/>
        private void ValidateUserProfile(UserProfileDetails userProfileDetails)
        {
            if (!PropertyValidator.IsValidLogin(userProfileDetails.LoginName))
            {
                throw new InputValidationException("Invalid login, it must be between 4 and 24 characters (Login=" + userProfileDetails.LoginName + ")");
            }
            if (!PropertyValidator.IsValidPassword(userProfileDetails.EnPassword))
            {
                throw new InputValidationException("Invalid password, it must be between 8 and 24 characters and contain at least a number and one upper case letter (Password=" + userProfileDetails.EnPassword + ")");
            }
            if (!PropertyValidator.IsValidFirstName(userProfileDetails.FirstName))
            {
                throw new InputValidationException("Invalid first name, it must be between 1 and 40 characters (FirstName=" + userProfileDetails.FirstName + ")");
            }
            if (!PropertyValidator.IsValidLastName(userProfileDetails.Lastname))
            {
                throw new InputValidationException("Invalid last name, it must be between 1 and 40 characters (LastName=" + userProfileDetails.Lastname + ")");
            }
            if (!PropertyValidator.IsValidEmail(userProfileDetails.Email))
            {
                throw new InputValidationException("Invalid email, it must be between 1 and 40 characters and contain an '@' before a '.' (Email=" + userProfileDetails.Email + ")");
            }
            if (!(userProfileDetails.Language is null) && !(userProfileDetails.Country is null) && !PropertyValidator.IsValidCulture(userProfileDetails.Language, userProfileDetails.Country))
            {
                throw new InputValidationException("Invalid culture, it must follow the ISO-639 for the language code and the ISO-3166 for the country code (Culture=" + userProfileDetails.Language + "-" + userProfileDetails.Country + ")");
            }
        }


    }
}
