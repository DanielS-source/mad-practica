using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Password
{
    public partial class Password : CulturePage
    {
        protected void OldPasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateOldPassword();
        }

        protected void NewPasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateNewPassword();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            ValidateOldPassword();
            ValidateNewPassword();

            if (IsValid)
            {
                try
                {
                    SessionManager.ChangePassword(Context, OldPasswordTextBox.Text, NewPasswordTextBox.Text);

                    Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
                }
                catch (IncorrectPasswordException)
                {
                    OldPasswordValidator.IsValid = false;
                    OldPasswordValidator.ErrorMessage = GetLocalResourceObject("OldPasswordValidator.ErrorMessage").ToString();
                }
            }
        }

        #region Private functions

        private void ValidateOldPassword()
        {
            OldPasswordValidator.IsValid = !string.IsNullOrWhiteSpace(OldPasswordTextBox.Text);
            if (OldPasswordValidator.IsValid)
            {
                OldPasswordValidator.ErrorMessage = GetLocalResourceObject("OldPasswordValidator.ErrorMessage").ToString();
            }
            else
            {
                OldPasswordValidator.ErrorMessage = GetLocalResourceObject("OldPasswordRequiredFieldValidator.ErrorMessage").ToString();
            }
        }

        private void ValidateNewPassword()
        {
            NewPasswordValidator.IsValid = PropertyValidator.IsValidPassword(NewPasswordTextBox.Text);
            if (!NewPasswordValidator.IsValid)
            {
                NewPasswordValidator.ErrorMessage = GetLocalResourceObject("NewPasswordValidator.ErrorMessage").ToString();
            }
            else if (string.IsNullOrWhiteSpace(NewPasswordConfirmTextBox.Text))
            {
                NewPasswordValidator.IsValid = false;
                NewPasswordValidator.ErrorMessage = GetLocalResourceObject("NewPasswordRequiredFieldValidator.ErrorMessage").ToString();
            }
            else if (!NewPasswordTextBox.Text.Equals(NewPasswordConfirmTextBox.Text))
            {
                NewPasswordValidator.IsValid = false;
                NewPasswordValidator.ErrorMessage = GetLocalResourceObject("NewPasswordCompareValidator.ErrorMessage").ToString();
            }
        }

        #endregion Private functions
    }
}