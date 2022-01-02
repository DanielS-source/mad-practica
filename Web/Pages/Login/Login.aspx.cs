using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using Web.Google;
using System.Net.Http;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Login
{
    public partial class Login : CulturePage
    {
        protected string googleplus_client_id = "367350001953-s261onetsralvvuhihdlc5alsgufb41f.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_secret = "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";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "https://localhost:44331/Pages/MainPage.aspx";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string Parameters;

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


        #region Google OAuth
        protected void Google_Click(object sender, EventArgs e)
        {
            var Googleurl = "https://accounts.google.com/o/oauth2/auth?response_type=code&redirect_uri=" + googleplus_redirect_url + "&scope=https://www.googleapis.com/auth/userinfo.email%20https://www.googleapis.com/auth/userinfo.profile&client_id=" + googleplus_client_id;
            Session["loginWith"] = "google";
            Response.Redirect(Googleurl);
        }

        #endregion Google OAuth

    }
}