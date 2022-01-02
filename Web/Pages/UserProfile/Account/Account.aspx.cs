using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Http.View;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Account
{
    public partial class Account : CulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails = SessionManager.FindUser(Context);
                FirstNameLastNameFieldLabel.Text = userProfileDetails.FirstName + " " + userProfileDetails.Lastname;
                EmailFieldLabel.Text = userProfileDetails.Email;
                CultureFieldLabel.Text = Languages.GetLanguage(userProfileDetails.Language) + " (" + Countries.GetCountry(userProfileDetails.Language, userProfileDetails.Country) + ")";
            }
        }

        protected void FirstNameLastNameModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/FirstNameLastName/FirstNameLastName.aspx"));
        }

        protected void EmailModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Email/Email.aspx"));
        }

        protected void PasswordModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Password/Password.aspx"));
        }

        protected void CultureModifyButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Culture/Culture.aspx"));
        }
    }
}