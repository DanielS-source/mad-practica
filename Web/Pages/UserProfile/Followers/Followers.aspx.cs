using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.UserProfile.Followers
{
    public partial class Followers : System.Web.UI.Page
    {
        private int startIndex = 0;
        private int pageSize = Properties.Settings.Default.page_size;

        public List<UserProfileDetails> followers = new List<UserProfileDetails>();

        UserBlock followersBlock;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.getFollowers(startIndex);
            }

        }

        protected void getFollowers(int startIndex)
        {
            IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            this.followersBlock = userService.GetFollowers(userId, startIndex, pageSize);

            this.followers = followersBlock.UserProfiles;
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {

        }

        protected void previousBtn_Click(object sender, EventArgs e)
        {

        }

        protected void render()
        {


            if (followers.Count <= 0 || startIndex <= 0)
            {
                previousBtn.Enabled = false;
            }

            if (followers.Count <= 0 || !followersBlock.ExistMoreUserProfiles)
            {
                nextBtn.Enabled = false;
            }


        }
    }

}
