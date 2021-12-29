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
            if (!IsPostBack)
            {
                Render_Session();
            }
        }

        //Función que controlará los elementos a renderizar en caso de usuario autenticado/o no
        public void Render_Session()
        {
            if (SessionManager.IsUserAuthenticated(Context)) //Usuario autenticado
            {
                LoginPanel.Visible = false;
                LoggoutPanel.Visible = true;
                UserProfileButton.Visible = true;
                UploadImagePanel.Visible = true;
            }
            else //Usuario sin autenticar
            {
                LoginPanel.Visible = true;
                LoggoutPanel.Visible = false;
                UserProfileButton.Visible = false;
                UploadImagePanel.Visible = false;
            }
        }

        protected void LogoutLinkButton_Click(object sender, EventArgs e)
        {
            SessionManager.Logout(Context);

            Response.Redirect("~/Pages/MainPage.aspx");
        }

        protected void UserProfileButton_Click(object sender, EventArgs e)
        {
           //Redirigir al perfil de usuario.
        }
    }
}