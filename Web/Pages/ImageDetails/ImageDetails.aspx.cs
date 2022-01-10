using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class ImageDetail : CulturePage
    {

        protected SearchImageDTO image;
        protected ImageDTO real_image;
        protected CommentsBlock comments;
        protected long userId;


        private const int TagPageSize = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();
            if (Request.QueryString["Image"] != null)
            {
                long imgId = Convert.ToInt64(Request.QueryString["Image"]);
                this.image = imageService.GetImageById(imgId);
                this.real_image = imageService.GetRealImageById(imgId);

                ViewState["image"] = imgId;
                ViewState["pathImage"] = @real_image.pathImg;
            }

            getComments();
            if (IsPostBack)
            {
                this.image = imageService.GetImageById(image.imgId);

                try //En caso de que haya un usuario logeado
                {
                    this.userId = SessionManager.GetUserId(Context);
                }
                catch (NullReferenceException) //En caso de que no haya un usuario logeado
                {}

            }
            if (!IsPostBack)
            {
                try //En caso de que haya un usuario logeado, comprobamos que sea el autor
                {

                    if (image.usrId == SessionManager.GetUserId(Context))
                    {
                        TagsContainer.Visible = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        TagsContainer.Visible = true;
                        btnDelete.Visible = true;
                    }

                }
                catch(NullReferenceException) //En caso de que no haya un usuario logeado
                {
                    TagsContainer.Visible = false;
                }

                LoadTags();

            }

        }

        protected void getComments() 
        {

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            this.comments = imageService.GetImageRelatedComments(image.imgId, CurrentCommentsPage, 5);
            CommRepeater.DataSource = this.comments.CommentList;
            CommRepeater.DataBind();

            render();
        }

        protected void nextBtn_Click(object sender, EventArgs e)
        {
            CurrentCommentsPage++;
            getComments();

        }

        protected void previousBtn_Click(object sender, EventArgs e)
        {
            CurrentCommentsPage--;
            getComments();

        }

        private int CurrentCommentsPage
        {
            get
            {
                if (ViewState["commentsPage"] is null)
                {
                    return 0;
                }
                else
                {
                    return (int)ViewState["commentsPage"];
                }
            }
            set
            {
                ViewState["commentsPage"] = value;
            }
        }

        protected void render()
        {
            previousBtn.Visible = true;
            nextBtn.Visible = true;

            if (comments.CommentList.Count <= 0 || CurrentCommentsPage <= 0)
            {
                previousBtn.Visible = false;
            }

            if (comments.CommentList.Count <= 0 || !comments.ExistMoreComments)
            {
                nextBtn.Visible = false;
            }


        }

        #region AddComment

        protected void Button1_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            try //En caso de que haya un usuario logeado, comprobamos que sea el autor
            {
                userId = SessionManager.GetUserId(Context);
                imageService.AddComment(userId, this.image.imgId, txtComment.Text);
            }
            catch (NullReferenceException) //En caso de que no haya un usuario logeado
            {
                Response.Redirect("~/Pages/Login/Login.aspx");
            }
        }

        #endregion AddComment

        #region LikeImage
        protected void LikeImage(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                long userId = SessionManager.GetUserId(Context);

                if (!imageService.isLiked(userId, image.imgId))
                {
                    imageService.LikeImage(userId, image.imgId);
                }
                else 
                {
                    imageService.UnlikeImage(userId, image.imgId);
                }

                Response.Redirect("~/Pages/ImageDetails/ImageDetails.aspx?Image="+image.imgId);
                
            }
        }
        #endregion LikeImage

        #region DeleteImage
        protected void DeleteImage(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                imageService.DeleteImage(image.imgId);
                Response.Redirect("~/Pages/MainPage/MainPage.aspx");
            }
        }
        #endregion DeleteImage

        #region DeleteComment
        protected void DeleteComment(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();
                try //En caso de que haya un usuario logeado, comprobamos que sea el autor
                {
                    string commentID = ((Button)sender).CommandArgument.ToString();
                    long commId = Convert.ToInt64(commentID);
                    imageService.DeleteComment(commId, userId);
                    Response.Redirect("~/Pages/MainPage/MainPage.aspx");
                }
                catch (NullReferenceException) //En caso de que no haya un usuario logeado
                {
                    Response.Redirect("~/Pages/Login/Login.aspx");
                }
            }
        }
        #endregion DeleteComment

        #region EditComment
        protected void EditComment(object sender, EventArgs e)
        {
            if (SessionManager.IsUserAuthenticated(Context))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                string commentID = ((Button)sender).CommandArgument.ToString();
                long commId = Convert.ToInt64(commentID);

                string val = editCommentText.Text;

                imageService.EditComment(commId, val);
                Response.Redirect("~/Pages/MainPage/MainPage.aspx");
            }
        }
        #endregion EditComment

        #region Tags
        protected void TagValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ValidateTag();
        }

        private void ValidateTag()
        {
            TagValidator.IsValid = PropertyValidator.IsValidTagName(TagTextBox.Text);
            TagValidator.ErrorMessage = GetLocalResourceObject("TagValidator.ErrorMessage").ToString();
        }

        protected void TagsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                TagDTO tagDTO = (TagDTO)e.Item.DataItem;

                Button tagButton = (Button)e.Item.FindControl("TagButton");
                tagButton.Text = tagDTO.Name;

                if (ExistsTag(tagDTO.TagId))
                {
                    tagButton.CssClass = "btn btn-primary";
                }
                else
                {
                    tagButton.CssClass = "btn btn-light";
                }
            }
        }

        private void AddTag(long tagId)
        {
            if (ViewState["tags"] is null)
            {
                ViewState["tags"] = new List<long>() {
                    { tagId }
                };
            }
            else
            {
                IList<long> tags = (IList<long>)ViewState["tags"];

                tags.Add(tagId);
            }
        }

        private bool ExistsTag(long tagId)
        {
            if (!(ViewState["tags"] is null))
            {
                IList<long> tags = (IList<long>)ViewState["tags"];

                return tags.Contains(tagId);
            }
            else
            {
                return false;
            }
        }

        private void RemoveTag(long tagId)
        {
            if (!(ViewState["tags"] is null))
            {
                IList<long> tags = (IList<long>)ViewState["tags"];

                tags.Remove(tagId);
            }
        }

        private short CurrentTagPage
        {
            get
            {
                if (ViewState["tagPage"] is null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt16(ViewState["tagPage"]);
                }
            }
            set
            {
                ViewState["tagPage"] = value;
            }
        }

        private IList<long> CurrentTags
        {
            get
            {
                if (ViewState["tags"] is null)
                {
                    return new List<long>();
                }
                else
                {
                    return (IList<long>)ViewState["tags"];
                }
            }
            set
            {
                ViewState["tags"] = value;
            }
        }

        private void LoadTags()
        {
            IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            TagBlock tagBlock = imageService.FindTags(TagPageSize, CurrentTagPage);

            TagsDataList.DataSource = tagBlock.TagDto;
            TagsDataList.DataBind();

            PreviousTagLinkButton.Enabled = tagBlock.HasPrevious;
            NextTagLinkButton.Enabled = tagBlock.HasNext;

            foreach(TagDTO tag in this.image.Tags)
            {
                TagsPanel.Text = TagsPanel.Text + " " + tag.Name.ToString();
            }
        }

        protected void TagButton_Click(object sender, EventArgs e)
        {
            Button tagButton = (Button)sender;
            long tagId = long.Parse(tagButton.CommandArgument);

            if (ExistsTag(tagId))
            {
                RemoveTag(tagId);
                tagButton.CssClass = "btn btn-light";
            }
            else
            {
                AddTag(tagId);
                tagButton.CssClass = "btn btn-primary";
            }

        }

        protected void AddTagButton_Click(object sender, EventArgs e)
        {
            ValidateTag();

            if (IsValidGroup("Tag"))
            {
                IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                try
                {
                    imageService.AddTag(TagTextBox.Text);
                    TagTextBox.Text = "";

                    LoadTags();
                }
                catch (DuplicateInstanceException)
                {
                    TagValidator.IsValid = false;
                    TagValidator.ErrorMessage = GetLocalResourceObject("TagCompareValidator.ErrorMessage").ToString();
                }
            }
        }




        protected void UpdateImageButton_Click(object sender, EventArgs e)
        {
            SessionManager.UpdateImage(Context, (long)ViewState["image"], (string)ViewState["pathImage"], CurrentTags);
            //Refresh
            LoadTags();
            Response.Redirect("~/Pages/ImageDetails/ImageDetails.aspx?Image="+ ViewState["image"]);
        }

        protected void PreviousTagLinkButton_Click(object sender, EventArgs e)
        {
            CurrentTagPage--;
            LoadTags();
        }

        protected void NextTagLinkButton_Click(object sender, EventArgs e)
        {
            CurrentTagPage++;
            LoadTags();
        }
        #endregion Tags
    }
}