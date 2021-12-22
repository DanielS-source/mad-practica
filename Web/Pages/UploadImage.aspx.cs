using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using Es.Udc.DotNet.ModelUtil.Log;

namespace Web.Pages
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Upload_Image(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

            //Display the Picture in Image control.
            Image1.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);
        }

        protected void BtnCreateClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                /* Create an Account. */
                long userIdentifier = Convert.ToInt32(txtUserId.Text);
                double balance = Convert.ToDouble(txtBalance.Text);

                Image img = new Image();
                String category = "";
                ImageDTO imageDTO = new ImageDTO(img, category);
                IList<long> tags= new List<long>();

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                Image createdImage = imageService.PostImage(imageDTO, tags);

                Context.Items.Add("Created Image", createdImage);

                LogManager.RecordMessage("Image  " + createdImage.imgId + " created.", MessageType.Info);

                Server.Transfer(Response.ApplyAppPathModifier("./MainPage.aspx"));
            }
        }
    }
}