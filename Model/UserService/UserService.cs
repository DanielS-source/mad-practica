using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        // void ChangePassword(long userProfileId, String oldClearPassword, String newClearPassword);
        public void ChangePassword(long userProfileId, string oldClearPassword, string newClearPassword) { }

        // UserProfileDetails FindUserProfileDetails(long userProfileId);
        public UserProfileDetails FindUserProfileDetails(long userProfileId) { return null; }

        // LoginResult Login(String loginName, String password, Boolean passwordIsEncrypted);
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted) { return null; }

        // long RegisterUser(String loginName, String clearPassword, UserProfileDetails userProfileDetails);
        public long RegisterUser(string loginName, string clearPassword,UserProfileDetails userProfileDetails) { return 0L; }

        // void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails);
        public void UpdateUserProfileDetails(long userProfileId, UserProfileDetails userProfileDetails) { }

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
    }
}
