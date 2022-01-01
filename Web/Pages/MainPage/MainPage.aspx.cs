using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = Es.Udc.DotNet.PracticaMaD.Model.Image;

namespace Web.Pages
{
    public partial class WebForm1 : CulturePage
    {
        public List<SearchImageDTO> images = new List<SearchImageDTO>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                this.initializeDropdown();
            }
        }

        #region CategoryDropdown
        void initializeDropdown()
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

        ICollection CreateDataSource(List<Category> categoryList)
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

        DataRow CreateRow(long catId, String Value, DataTable dt)
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
        #endregion CategoryDropdown

        #region SearchImages

        protected void SearchImages(object sender, EventArgs e)
        {
            if (IsValidGroup("Required"))
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                images = imageService.SearchImages(keywordsInput.Text, categoryDropDown.SelectedItem.Text, 0, 10).Images;

                for (int i = 0; i < images.Count; i++)
                {
                    string imreBase64Data = Convert.ToBase64String(images[0].file);
                    string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                    images[i].imageSrc = imgDataURL;
                }
            }
        }


        #endregion SearchImages
    }
}