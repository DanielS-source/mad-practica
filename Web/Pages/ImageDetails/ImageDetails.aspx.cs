using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Web;

namespace Web.Pages
{
    public partial class ImageDetail : CulturePage
    {

        protected SearchImageDTO image;
        protected CommentsBlock comments;

        protected void Page_Load(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();
            if (Request.QueryString["Image"] != null)
            {
                long imgId = Convert.ToInt64(Request.QueryString["Image"]);
                this.image = imageService.GetImageById(imgId);
            }

            this.comments = imageService.GetImageRelatedComments(image.imgId, 0, 10);
            if (IsPostBack)
            {
                this.image = imageService.GetImageById(image.imgId);
            }
        }


        #region AddComment

        protected void Button1_Click(object sender, EventArgs e)
        {
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();
            long userId = 1L;//SessionManager.GetUserId(Context);
            imageService.AddComment(userId, this.image.imgId, txtComment.Text);
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
                imageService.LikeImage(userId, image.imgId);
            }
        }

        #endregion LikeImage
    }
}