using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.Security;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile
{
    public partial class UserProfile : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UserProfileDetails userProfileDetails = SessionManager.FindUser(Context);
                    NameLabel.Text = userProfileDetails.FirstName;
                }
                catch (AuthenticationException)
                {
                    UserProfileMainPanel.Visible = false;
                }

            }
        }

        protected void AccountLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
        }

        protected void FollowersFollowsButton_Click(object sender, EventArgs e)
        {
            try
            {
                long usrId = SessionManager.GetUserId(Context);
                String url =
                    String.Format("../Follows/Follows.aspx?userId={0}", usrId);

                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
            catch(NullReferenceException)
            {
                UserProfileMainPanel.Visible = false;
            }

        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            SessionManager.Logout(Context);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage/MainPage.aspx"));
        }
    }
}