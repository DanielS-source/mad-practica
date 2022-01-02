using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile
{
    public partial class UserProfile : MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            UserProfileDetails userProfileDetails = SessionManager.FindUser(Context);
            NameLabel.Text = userProfileDetails.FirstName;
        }

        protected void AccountLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
        }

        protected void FollowersFollowsButton_Click(object sender, EventArgs e)
        {

            long usrId = SessionManager.GetUserId(Context);
            String url =
                String.Format("../Followers/Followers.aspx?userId={0}", usrId);

            Response.Redirect(url);

        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/MainPage/MainPage.aspx");
        }
    }
}