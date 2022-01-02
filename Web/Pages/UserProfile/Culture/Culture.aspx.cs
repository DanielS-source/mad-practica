using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Http.View;
using System;
using System.Globalization;
using System.Threading;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.UserProfile.Culture
{
    public partial class Culture : CulturePage
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

        protected void LanguageDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo(LanguageDropDownList.SelectedValue + "-" + CountryDropDownList.SelectedValue);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InfoLabel.Text = GetLocalResourceObject("InfoLabel.Text").ToString();
            SaveButton.Text = GetLocalResourceObject("SaveButton.Text").ToString();

            UpdateLanguageDropDownList(LanguageDropDownList.SelectedValue);
            UpdateCountryDropDownList(LanguageDropDownList.SelectedValue, CountryDropDownList.SelectedValue);
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SessionManager.ChangeCulture(Context, LanguageDropDownList.SelectedValue, CountryDropDownList.SelectedValue);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UserProfile/Account/Account.aspx"));
        }

        #region Private functions

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

        #endregion Private functions
    }
}