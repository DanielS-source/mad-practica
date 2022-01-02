using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.FirstNameLastName
{
    public partial class FirstNameLastName : CulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserProfileDetails userProfileDetails = SessionManager.FindUser(Context);
                FirstNameTextBox.Text = userProfileDetails.FirstName;
                LastNameTextBox.Text = userProfileDetails.Lastname;
            }
        }

        protected void FirstNameLastNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateFirstNameLastName();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ValidateFirstNameLastName();

            if (IsValid)
            {
                SessionManager.ChangeFirstNameLastName(Context, FirstNameTextBox.Text, LastNameTextBox.Text);

                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
            }
        }

        #region Private functions

        private void ValidateFirstNameLastName()
        {
            FirstNameLastNameValidator.IsValid = PropertyValidator.IsValidFirstName(FirstNameTextBox.Text) && PropertyValidator.IsValidLastName(LastNameTextBox.Text);
            FirstNameLastNameValidator.ErrorMessage = GetLocalResourceObject("FirstNameLastNameValidator.ErrorMessage").ToString();
        }

        #endregion Private functions
    }
}