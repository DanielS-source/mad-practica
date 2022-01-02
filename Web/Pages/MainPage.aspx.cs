using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace Web.Pages
{
 
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected string googleplus_client_id = "367350001953-s261onetsralvvuhihdlc5alsgufb41f.apps.googleusercontent.com";    // Replace this with your Client ID
        protected string googleplus_client_secret = "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";                                                // Replace this with your Client Secret
        protected string googleplus_redirect_url = "https://localhost:44331/Pages/MainPage.aspx";                                         // Replace this with your Redirect URL; Your Redirect URL from your developer.google application should match this URL.
        protected string Parameters;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                categoryDropDown.DataSource = CreateDataSource();
                categoryDropDown.DataTextField = "ColorTextField";
                categoryDropDown.DataValueField = "ColorValueField";

                categoryDropDown.DataBind();

                categoryDropDown.SelectedIndex = 0;
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
                                    Response.Redirect("~/Pages/MainPage.aspx");
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //throw new Exception(ex.Message, ex);
                    Response.Redirect("~/Pages/MainPage.aspx");
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
                            Response.Redirect("~/Pages/MainPage.aspx");
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

        ICollection CreateDataSource()
        {

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ColorTextField", typeof(String)));
            dt.Columns.Add(new DataColumn("ColorValueField", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(CreateRow("White", "White", dt));
            dt.Rows.Add(CreateRow("Silver", "Silver", dt));
            dt.Rows.Add(CreateRow("Dark Gray", "DarkGray", dt));
            dt.Rows.Add(CreateRow("Khaki", "Khaki", dt));
            dt.Rows.Add(CreateRow("Dark Khaki", "DarkKhaki", dt));

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }
    }
}