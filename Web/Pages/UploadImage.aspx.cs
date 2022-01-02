using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.ModelUtil.IoC;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.Collections;
using System.Data;

namespace Web.Pages
{
    public partial class WebForm3 : CulturePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.initializeDropdown();
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

        protected void PostImage(object sender, EventArgs e)
        {

            if (IsValidGroup("Required"))
            {
                Trace.IsEnabled = true;
                /* Create the Image */
                ImageDTO image = new ImageDTO
                {
                    /* GENERAL data */
                    usrId = 1L,
                    title = txtTitle.Text,
                    description = txtDescription.Text,
                    dateImg = DateTime.Now,
                    category = categoryDropDown.SelectedItem.Text,
                    /* EXIF data */
                    f = txtDiaphragmAperture.Text,
                    t = txtShutterSpeed.Text,
                    ISO = txtISO.Text,
                    wb = txtWhiteBalance.Text,
                    file = FileUpload1.FileBytes
                };
                LogManager.RecordMessage("Image  " + image.title + " created.", MessageType.Info);
                /* Create the Tags */
                List<long> tags = new List<long>();

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                /* Post Image */
                Image createdImage = imageService.PostImage(image, tags);

                Response.Redirect("~/Pages/MainPage/MainPage.aspx");
            }
        }
        protected void initializeDropdown()
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

        protected ICollection CreateDataSource(List<Category> categoryList)
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

        protected DataRow CreateRow(long catId, String Value, DataTable dt)
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
    }
}
