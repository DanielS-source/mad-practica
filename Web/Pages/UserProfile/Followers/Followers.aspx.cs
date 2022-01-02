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
        public int startIndex;
        private int pageSize = Properties.Settings.Default.page_size;

        public List<UserProfileDetails> followers = new List<UserProfileDetails>();
        
        UserBlock followersBlock;

        protected void Page_Load(object sender, EventArgs e)
        {

            startIndex = Convert.ToInt32(Request.Params.Get("startIndex"));
            this.getFollowers();
            this.render();

        }

        protected void getFollowers()
        {
            IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));
            int operation = Convert.ToInt32(Request.Params.Get("op"));
            if (operation == 0)
            {
                this.followersBlock = userService.GetFollowers(userId, startIndex, pageSize);
            }
            else 
            {
                this.followersBlock = userService.GetFollowed(userId, startIndex, pageSize);
            }
                
            
            this.followers = followersBlock.UserProfiles;
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));
            int operation = Convert.ToInt32(Request.Params.Get("op"));
            startIndex = startIndex + 1;
            String url =
                String.Format("../Followers/Followers.aspx?userId={0}&op={1}&=startIndex={2}", userId, operation, startIndex);

            Response.Redirect(url);
        }

        protected void previousBtn_Click(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));
            int operation = Convert.ToInt32(Request.Params.Get("op"));

            String url =
                String.Format("../Followers/Followers.aspx?userId={0}&op={1}&=startIndex={2}", userId, operation, startIndex--);

            Response.Redirect(url);
        }

        protected void render()
        {
            previousBtn.Visible = true;
            nextBtn.Visible = true;

            if (followers.Count <= 0 || startIndex <= 0)
            {
                previousBtn.Visible = false;
            }

            if (followers.Count <= 0 || !followersBlock.ExistMoreUserProfiles)
            {
                nextBtn.Visible = false;
            }


        }
    }

}
