using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Http.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Register
{
    public partial class Register : CulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Locale locale = SessionManager.GetLocale(Context);
                UpdateLanguageDropDownList(locale.Language);
                UpdateCountryDropDownList(locale.Language, locale.Country);
            }
        }

        #region ServerValidators

        protected void UsernameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateUsername();
        }

        protected void FirstNameLastNameValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateFirstNameLastName();
        }

        protected void EmailValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateEmail();
        }

        protected void PasswordValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidatePassword();
        }

        #endregion ServerValidators


        #region ButtonClicks
        protected void LoginLinkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Request.Params.Get("returnUrl")))
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Login/Login.aspx"));
            }
            else
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Login/Login.aspx?returnUrl=" + Request.Params.Get("returnUrl")));
            }
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {

            ValidateUsername();
            ValidateFirstNameLastName();
            ValidateEmail();
            ValidatePassword();

            if (IsValidGroup("Required"))
            {
                Required.Visible = false;

                ViewState["Username"] = UsernameTextBox.Text;
                ViewState["Password"] = PasswordTextBox.Text;
                ViewState["FirstName"] = FirstNameTextBox.Text;
                ViewState["LastName"] = LastNameTextBox.Text;
                ViewState["Email"] = EmailTextBox.Text;

                SessionManager.RegisterUser(Context, (string)ViewState["Username"], (string)ViewState["Password"],
                new UserProfileDetails(
                    (string)ViewState["Username"],
                    (string)ViewState["Password"],
                    (string)ViewState["FirstName"],
                    (string)ViewState["LastName"],
                    (string)ViewState["Email"],
                    LanguageDropDownList.SelectedValue,
                    CountryDropDownList.SelectedValue
                ));
                FormsAuthentication.RedirectFromLoginPage(UsernameTextBox.Text, true);
            }

        }

        #endregion ButtonClicks


        #region Private functions

        protected void LanguageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo(LanguageDropDownList.SelectedValue + "-" + CountryDropDownList.SelectedValue);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            UpdateLanguageDropDownList(LanguageDropDownList.SelectedValue);
            UpdateCountryDropDownList(LanguageDropDownList.SelectedValue, CountryDropDownList.SelectedValue);

            LanguageLabel.Text = GetLocalResourceObject("LanguageLabel.Text").ToString();
            CountryLabel.Text = GetLocalResourceObject("CountryLabel.Text").ToString();

            RegisterButton.Text = GetLocalResourceObject("RegisterButton.Text").ToString();
        }


        private void UpdateLanguageDropDownList(string selectedLanguage)
        {
            LanguageDropDownList.DataSource = Languages.GetLanguages(selectedLanguage);
            LanguageDropDownList.DataTextField = "text";
            LanguageDropDownList.DataValueField = "value";
            LanguageDropDownList.DataBind();
            LanguageDropDownList.SelectedValue = selectedLanguage;
        }

        private void UpdateCountryDropDownList(string selectedLanguage, string selectedCountry)
        {
            CountryDropDownList.DataSource = Countries.GetCountries(selectedLanguage);
            CountryDropDownList.DataTextField = "text";
            CountryDropDownList.DataValueField = "value";
            CountryDropDownList.DataBind();
            CountryDropDownList.SelectedValue = selectedCountry;
        }

        private void ValidateUsername()
        {
            UsernameValidator.IsValid = PropertyValidator.IsValidLogin(UsernameTextBox.Text);

            if (UsernameValidator.IsValid)
            {
                IIoCManager iocManager = (IIoCManager)Application["ManagerIoC"];
                IUserService userService = iocManager.Resolve<IUserService>();

                UsernameValidator.IsValid = !userService.UserExists(UsernameTextBox.Text);
                UsernameValidator.ErrorMessage = GetLocalResourceObject("UsernameCompareValidator.ErrorMessage").ToString();
            }
            else
            {
                UsernameValidator.ErrorMessage = GetLocalResourceObject("UsernameValidator.ErrorMessage").ToString();
            }
        }

        private void ValidateFirstNameLastName()
        {
            FirstNameLastNameValidator.IsValid = PropertyValidator.IsValidFirstName(FirstNameTextBox.Text) && PropertyValidator.IsValidLastName(LastNameTextBox.Text);
            FirstNameLastNameValidator.ErrorMessage = GetLocalResourceObject("FirstNameLastNameValidator.ErrorMessage").ToString();
        }

        private void ValidateEmail()
        {
            EmailValidator.IsValid = PropertyValidator.IsValidEmail(EmailTextBox.Text);
            EmailValidator.ErrorMessage = GetLocalResourceObject("EmailValidator.ErrorMessage").ToString();
        }


        private void ValidatePassword()
        {
            PasswordValidator.IsValid = PropertyValidator.IsValidPassword(PasswordTextBox.Text);
            if (!PasswordValidator.IsValid)
            {
                PasswordValidator.ErrorMessage = GetLocalResourceObject("PasswordValidator.ErrorMessage").ToString();
            }
            else if (string.IsNullOrWhiteSpace(PasswordConfirmTextBox.Text))
            {
                PasswordValidator.IsValid = false;
                PasswordValidator.ErrorMessage = GetLocalResourceObject("PasswordRequiredFieldValidator.ErrorMessage").ToString();
            }
            else if (!PasswordTextBox.Text.Equals(PasswordConfirmTextBox.Text))
            {
                PasswordValidator.IsValid = false;
                PasswordValidator.ErrorMessage = GetLocalResourceObject("PasswordCompareValidator.ErrorMessage").ToString();
            }
        }

        #endregion Private functions
    }
}