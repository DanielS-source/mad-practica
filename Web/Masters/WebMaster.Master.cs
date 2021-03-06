using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Render_Session();

            if (!IsPostBack)
            {
                
            }
        }

        //Función que controlará los elementos a renderizar en caso de usuario autenticado/o no
        public void Render_Session()
        {
            if (SessionManager.IsUserAuthenticated(Context)) //Usuario autenticado
            {
                LoginPanel.Visible = false;
                LoggoutPanel.Visible = true;
            }
            else //Usuario sin autenticar
            {
                LoginPanel.Visible = true;
                LoggoutPanel.Visible = false;
            }

            UserProfileButton.Visible = true;
        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            SessionManager.Logout(Context);

            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/MainPage/MainPage.aspx"));
        }

        protected void UserProfileButton_Click(object sender, EventArgs e)
        {
            //Redirigir al perfil de usuario.

            if (SessionManager.IsUserAuthenticated(Context))
            {

                long usrId = SessionManager.GetUserId(Context);
                String url =
                    String.Format("~/Pages/UserProfile/Account/Account.aspx?userId={0}", usrId);

                Response.Redirect(Response.ApplyAppPathModifier(url));

            }
            else 
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Login/Login.aspx"));
            }
        }

        protected void UploadImageButton_Click(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context)) //Usuario autenticado
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/UploadImage/UploadImage.aspx"));
            }
            else
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Login/Login.aspx"));
            }    
        }


    }
}