using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void btnFollowed_Click(object sender, EventArgs e)
        {
            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            String url =
                String.Format("../Followers/Followers.aspx?userId={0}&op={1}", userId, 1);

            Response.Redirect(Response.ApplyAppPathModifier(url));
        }

        protected void getUserData() 
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            long userId = Convert.ToInt32(Request.Params.Get("userId"));

            UserProfileDetails user = userService.FindUserProfileDetails(userId);

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

        protected void ImagesDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                SearchImageDTO imageDto = (SearchImageDTO)e.Item.DataItem;

                HyperLink autorLink = (HyperLink)e.Item.FindControl("AutorLink");
                autorLink.Text = "<i class='icon-location-arrow pr-1'> " + imageDto.username + " </i>";
                autorLink.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/UserProfile/Follows/Follows.aspx?userId=" + imageDto.usrId);

                HyperLink detailsLink = (HyperLink)e.Item.FindControl("DetailsLink");
                detailsLink.Text = "<i class='icon-ellipsis-horizontal pr-1'> See More </i>";
                detailsLink.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/ImageDetails/ImageDetails.aspx?Image=" + imageDto.imgId);

                HyperLink commentsLink = (HyperLink)e.Item.FindControl("CommentsLink");
                commentsLink.Text = "<i class='icon-comments  pr-1'> " + imageDto.nComments + " </i>";
                commentsLink.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/ImageDetails/ImageDetails.aspx?Image=" + imageDto.imgId); //Links A Comments

                HyperLink likesLink = (HyperLink)e.Item.FindControl("LikesLink");
                likesLink.Text = "<i class='icon-thumbs-up pr-1'> " + imageDto.likes + " </i>";
                likesLink.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/ImageDetails/ImageDetails.aspx?Image=" + imageDto.imgId); //Links A Likes

                HtmlGenericControl dateLabel = (HtmlGenericControl)e.Item.FindControl("DateLabel");
                dateLabel.InnerText = imageDto.dateImg.ToString();

                HtmlGenericControl imageTitle = (HtmlGenericControl)e.Item.FindControl("ImageTitle");
                imageTitle.InnerText = imageDto.title;

                Label imageDescription = (Label)e.Item.FindControl("ImageDescription");
                imageDescription.Text = imageDto.description;

                System.Web.UI.WebControls.Image imageImagen = (System.Web.UI.WebControls.Image)e.Item.FindControl("ImageImagen");

                string imreBase64Data = Convert.ToBase64String(imageDto.file);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                imageImagen.ImageUrl = imgDataURL;

            }
        }

        protected void followingBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Login/Login.aspx"));
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


            ImagesDataList.DataSource = images;
            ImagesDataList.DataBind();

        }
    }
}