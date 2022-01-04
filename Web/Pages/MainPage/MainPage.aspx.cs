﻿using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using System.Web.Security;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using Web.Google;
using System.Net.Http;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;

namespace Web.Pages
{
    public partial class WebForm1 : CulturePage
    {
        public List<SearchImageDTO> images = new List<SearchImageDTO>();

        protected string googleplus_client_id = "367350001953-s261onetsralvvuhihdlc5alsgufb41f.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_secret = "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";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "https://localhost:44331/Pages/MainPage/MainPage.aspx";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string Parameters;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                this.initializeDropdown();
            }

            //Esto es parte del Google OAuth
            if ((Session.Contents.Count > 0) && (Session["loginWith"] != null) && (Session["loginWith"].ToString() == "google"))
            {
                try
                {
                    var url = Request.Url.Query;

                    if (url != "")
                    {
                        string queryString = url.ToString();
                        char[] delimiterChars = { '=' };
                        string[] words = queryString.Split(delimiterChars);
                        string code = words[1];


                        if (code != null)
                        {
                            //get the access token 
                            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                            webRequest.Method = "POST";
                            Parameters = "code=" + code + "&client_id=" + googleplus_client_id + "&client_secret=" + googleplus_client_secret + "&redirect_uri=" + googleplus_redirect_url + "&grant_type=authorization_code";
                            byte[] byteArray = Encoding.UTF8.GetBytes(Parameters);
                            webRequest.ContentType = "application/x-www-form-urlencoded";
                            webRequest.ContentLength = byteArray.Length;
                            Stream postStream = webRequest.GetRequestStream();
                            // Add the post data to the web request
                            postStream.Write(byteArray, 0, byteArray.Length);
                            postStream.Close();

                            WebResponse response = webRequest.GetResponse();
                            postStream = response.GetResponseStream();
                            StreamReader reader = new StreamReader(postStream);
                            string responseFromServer = reader.ReadToEnd();

                            GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                            if (serStatus != null)
                            {
                                string accessToken = string.Empty;
                                accessToken = serStatus.access_token;

                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    // This is where you want to add the code if login is successful.
                                    getgoogleplususerdataSer(accessToken);
                                    Response.Redirect("~/Pages/MainPage/MainPage.aspx");
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex);
                    Response.Redirect("~/Pages/MainPage/MainPage.aspx");
                }
            }
            //********Fin del Google OAuth********

        }

        #region Google OAuth
        private async void getgoogleplususerdataSer(string access_token)
        {
            try
            {
                HttpClient client = new HttpClient();
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;

                client.CancelPendingRequests();
                HttpResponseMessage output = await client.GetAsync(urlProfile);

                if (output.IsSuccessStatusCode)
                {
                    string outputData = await output.Content.ReadAsStringAsync();
                    GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

                    if (serStatus != null)
                    {
                        var googleUserName = serStatus.id.Substring(0, 5) + serStatus.email.Substring(0, 5);
                        var googlePass = "GooglePass1";

                        //----------------Aquí podemos guardarla en la BD por ej.----------
                        try
                        {
                            SessionManager.Login(Context, googleUserName, googlePass, false);
                            Response.Redirect("~/Pages/MainPage/MainPage.aspx");
                        }
                        catch (InstanceNotFoundException)
                        {
                            SessionManager.RegisterUser(Context, googleUserName, googlePass,
                            new UserProfileDetails(
                               googleUserName,
                               googlePass,
                               serStatus.name,
                               serStatus.given_name,
                               serStatus.email,
                               "es",
                               "ES"
                           ));

                            SessionManager.Login(Context, googleUserName, googlePass, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //catching the exception
            }

        }
        #endregion Google OAuth

        #region CategoryDropdown
        void initializeDropdown()
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            List<Category> categoryList = imageService.GetAllCategories();

            categoryDropDown.DataSource = CreateDataSource(categoryList);
            categoryDropDown.DataTextField = "name";
            categoryDropDown.DataValueField = "catId";

            categoryDropDown.DataBind();

            categoryDropDown.SelectedIndex = 0;
        }

        ICollection CreateDataSource(List<Category> categoryList)
        {

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("name", typeof(String)));
            dt.Columns.Add(new DataColumn("catId", typeof(long)));

            // Populate the table with sample values.

            foreach (Category c in categoryList)
            {
                dt.Rows.Add(CreateRow(c.catId, c.name, dt));
            }

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(long catId, String Value, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = Value;
            dr[1] = catId;

            return dr;

        }
        #endregion CategoryDropdown

        #region SearchImages

        protected void SearchImages(object sender, EventArgs e)
        {
            if (IsValidGroup("Required"))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                images = imageService.SearchImages(keywordsInput.Text, categoryDropDown.SelectedItem.Text, 0, 10).Images;

                for (int i = 0; i < images.Count; i++)
                {
                    string imreBase64Data = Convert.ToBase64String(images[0].file);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    images[i].imageSrc = imgDataURL;
                }
            }
        }


        #endregion SearchImages
    }
}