using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                getUserData();
            Console.Write("wkrgnlkwrmg");

        }

        protected void btnFollowers_Click(object sender, EventArgs e)
        {

        }

        protected void btnFollowed_Click(object sender, EventArgs e)
        {

        }

        protected void getUserData() 
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            UserProfileDetails user = userService.FindUserProfileDetails(userId);

            Console.WriteLine(user);
            cellAccountID.Text = user.Email;
            userContainer.InnerHtml = "<h1 class=\"text-center\">" + user.LoginName + "</h1>";
        }
    }
}