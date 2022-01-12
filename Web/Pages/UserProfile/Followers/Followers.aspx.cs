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
        private int pageSize = Properties.Settings.Default.page_size;

        public List<UserProfileDetails> followers = new List<UserProfileDetails>();
        
        UserBlock followersBlock;

        protected void Page_Load(object sender, EventArgs e)
        {

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
                this.followersBlock = userService.GetFollowers(userId, CurrentFollowPage, pageSize);
            }
            else 
            {
                this.followersBlock = userService.GetFollowed(userId, CurrentFollowPage, pageSize);
            }
                
            
            this.followers = followersBlock.UserProfiles;

            FollowersDataList.DataSource = this.followers;
            FollowersDataList.DataBind();
        }

        protected void FollowsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                UserProfileDetails userProfile = (UserProfileDetails)e.Item.DataItem;

                HyperLink followLink = (HyperLink)e.Item.FindControl("FollowLink");
                followLink.Text = userProfile.FirstName;
                followLink.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/UserProfile/Follows/Follows.aspx?userId=" + userProfile.usrId);
            }
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            CurrentFollowPage++;
            getFollowers();
            render();
        }

        protected void previousBtn_Click(object sender, EventArgs e)
        {
            CurrentFollowPage--;
            getFollowers();
            render();
        }

        protected void render()
        {
            previousBtn.Visible = true;
            nextBtn.Visible = true;

            if (followers.Count <= 0 || CurrentFollowPage <= 0)
            {
                previousBtn.Visible = false;
            }

            if (followers.Count <= 0 || !followersBlock.ExistMoreUserProfiles)
            {
                nextBtn.Visible = false;
            }


        }

        private int CurrentFollowPage
        {
            get
            {
                if (ViewState["followPage"] is null)
                {
                    return 0;
                }
                else
                {
                    return (int)ViewState["followPage"];
                }
            }
            set
            {
                ViewState["followPage"] = value;
            }
        }
    }

}
