using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Email
{
    public partial class Email : CulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails = SessionManager.FindUser(Context);
                EmailTextBox.Text = userProfileDetails.Email;
            }
        }

        protected void EmailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateEmail();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ValidateEmail();

            if (IsValid)
            {
                SessionManager.ChangeEmail(Context, EmailTextBox.Text);

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
            }
        }

        #region Private functions

        private void ValidateEmail()
        {
            EmailValidator.IsValid = PropertyValidator.IsValidEmail(EmailTextBox.Text);
            EmailValidator.ErrorMessage = GetLocalResourceObject("EmailValidator.ErrorMessage").ToString();
        }

        #endregion Private functions
    }
}