using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserBlock
    {
        public List<UserProfileDetails> UserProfiles { get; private set; }
        public bool ExistMoreUserProfiles { get; private set; }

        public UserBlock(List<UserProfileDetails> userProfiles, bool existMoreUserProfiles)
        {
            this.UserProfiles = UserProfiles;
            this.ExistMoreUserProfiles = existMoreUserProfiles;
        }
    }
}
