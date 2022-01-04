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
using System.Web.UI.WebControls;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Utils;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Web.Pages
{
    public partial class WebForm3 : CulturePage
    {

        private const int TagPageSize = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.initializeDropdown();

            if (!IsPostBack)
            {

                /*---TAGS---*/

                LoadTags();

                /*---ENDTAGS---*/
            }

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
                //List<long> tags = new List<long>();

                /* Get the Service */
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                /* Post Image */
                Image createdImage = imageService.PostImage(image, CurrentTags);

                /* Log methods */
                Context.Items.Add("Created Image", createdImage);
                LogManager.RecordMessage("Image  " + createdImage.imgId + " created.", MessageType.Info);
                Console.WriteLine(createdImage);
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

            LoadTags();
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

        private void RemoveTag(long tagId)
        {
            if (!(ViewState["tags"] is null))
            {
                IList<long> tags = (IList<long>)ViewState["tags"];

                tags.Remove(tagId);
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

        protected void TagsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                TagDTO tagDto = (TagDTO)e.Item.DataItem;

                Button tagButton = (Button)e.Item.FindControl("TagButton");
                tagButton.Text = tagDto.Name;

                if (ExistsTag(tagDto.TagId))
                {
                    tagButton.CssClass = "btn btn-primary";
                }
                else
                {
                    tagButton.CssClass = "btn btn-light";
                }
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
        }

        #endregion Tags

    }
}
