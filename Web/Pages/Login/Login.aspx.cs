using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Login
{
    public partial class Login : CulturePage
    {
        protected void UsernameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateUsername();
        }

        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidatePassword();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            ValidateUsername();
            ValidatePassword();

            if (IsValid)
            {
                try
                {
                    SessionManager.Login(Context, UsernameTextBox.Text, PasswordTextBox.Text, RememberCheckBox.Checked);

                    FormsAuthentication.RedirectFromLoginPage(UsernameTextBox.Text, RememberCheckBox.Checked);
                }
                catch (InstanceNotFoundException)
                {
                    UsernameValidator.IsValid = false;
                    UsernameValidator.ErrorMessage = GetLocalResourceObject("UsernameValidator.ErrorMessage").ToString();
                }
                catch (IncorrectPasswordException)
                {
                    PasswordValidator.IsValid = false;
                    PasswordValidator.ErrorMessage = GetLocalResourceObject("PasswordValidator.ErrorMessage").ToString();
                }
            }
        }

        protected void RegisterLinkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.Params.Get("returnUrl")))
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Register/Register.aspx"));
            }
            else
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Register/Register.aspx?returnUrl=" + Request.Params.Get("returnUrl")));
            }
        }

        #region Private functions

        private void ValidateUsername()
        {
            UsernameValidator.IsValid = !string.IsNullOrWhiteSpace(UsernameTextBox.Text);
            UsernameValidator.ErrorMessage = GetLocalResourceObject("UsernameRequiredFieldValidator.ErrorMessage").ToString();
        }

        private void ValidatePassword()
        {
            PasswordValidator.IsValid = !string.IsNullOrWhiteSpace(PasswordTextBox.Text);
            PasswordValidator.ErrorMessage = GetLocalResourceObject("PasswordRequiredFieldValidator.ErrorMessage").ToString();
        }

        #endregion Private functions

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}