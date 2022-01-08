using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
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
        public List<SearchImageDTO> images = new List<SearchImageDTO>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                getUserData();
                renderNonFollowing();
                SearchImages();
            }

        }

        protected void btnFollowers_Click(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            String url =
                String.Format("../Followers/Followers.aspx?userId={0}&op={1}", userId,0);

            Response.Redirect(url);
        }

        protected void btnFollowed_Click(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            String url =
                String.Format("../Followers/Followers.aspx?userId={0}&op={1}", userId, 1);

            Response.Redirect(url);
        }

        protected void getUserData() 
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            UserProfileDetails user = userService.FindUserProfileDetails(userId);

            Console.WriteLine(user);
            cellAccountID.Text = user.Email;
            userContainer.InnerHtml = "<h1 class=\"text-center\">" + user.FirstName + "</h1>";

            try
            {
                if (SessionManager.GetUserId(Context) == userId)
                {

                }
            }
            catch(NullReferenceException)
            {}

        }

        protected void followingBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
            }
            else
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IUserService userService = iocManager.Resolve<IUserService>();

                long userId = Convert.ToInt32(Request.Params.Get("userId"));

                userService.FollowUser(SessionManager.GetUserId(Context), userId);

                this.renderNonFollowing();
            }
        }

        protected void renderNonFollowing()
        {

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));


            try
            {
                bool isFollowing = userService.isFollowing(SessionManager.GetUserId(Context), userId);

                if (SessionManager.GetUserId(Context) == userId)
                {
                    followDiv.Visible = false;

                }
                else 
                {
                    followBtn.Visible = false;
                    followingBtn.Visible = false;

                    if (!isFollowing)
                    {

                        //followingBtn.Text = "\" <%$Resources: , follow.Text %> \"";
                        //followingBtn.CssClass = "btn btn-success";
                        followBtn.Visible = true;
                        backgroundSpan.Attributes.Remove("class");
                        backgroundSpan.Attributes.Add("class", "input-group-text bg-success");
                    }
                    else
                    {
                        //followingBtn.Text = "\" <%$Resources: , followingBtn.Text %> \"";
                        //followingBtn.CssClass = "btn btn-secondary";
                        followingBtn.Visible = true;
                        followingBtn.Enabled = false;

                        backgroundSpan.Attributes.Remove("class");
                        backgroundSpan.Attributes.Add("class", "input-group-text bg-secondary");

                    }
                }

            }
            catch (NullReferenceException)
            {
                followBtn.Visible = true;
                followingBtn.Visible = false;
                backgroundSpan.Attributes.Remove("class");
                backgroundSpan.Attributes.Add("class", "input-group-text bg-success");
            }
        }

        protected void SearchImages()
        {
            
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            images = imageService.FinByUser(userId, 3).Images;

            for (int i = 0; i < images.Count; i++)
            {
                string imreBase64Data = Convert.ToBase64String(images[i].file);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                images[i].imageSrc = imgDataURL;
            }
        }
    }
}