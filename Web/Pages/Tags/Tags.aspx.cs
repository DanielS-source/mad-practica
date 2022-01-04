using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Tags
{
    public partial class Tags : CulturePage
    {
        private const int TagPageSize = 5;
        private const int ImagePageSize = 2;

        private int maxImage = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTags();
                LoadImages();
            }
        }

        protected void TagsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                TagDTO tagDto = (TagDTO)e.Item.DataItem;

                Button tagButton = (Button)e.Item.FindControl("TagButton");
                tagButton.Text = tagDto.Name;
                tagButton.Font.Size = new FontUnit(ScaleBetween(tagDto.Uses, 10, 30, 0, maxImage + 1));
            }
        }

        protected void TagButton_Click(object sender, EventArgs e)
        {
            Button tagButton = (Button)sender;
            ViewState["tag"] = long.Parse(tagButton.CommandArgument);

            CurrentImagePage = 0;
            LoadImages();
        }

        protected void ImagesDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                ImageWithTagsDto imageDto = (ImageWithTagsDto)e.Item.DataItem;

                Label loginLabel = (Label)e.Item.FindControl("LoginLabel");
                loginLabel.Text = imageDto.LoginName;

                Label dateLabel = (Label)e.Item.FindControl("DateLabel");
                dateLabel.Text = imageDto.dateImg.ToString("d");

                TextBox textTextBox = (TextBox)e.Item.FindControl("TextTextBox");
                textTextBox.Text = imageDto.title;

                Image imageImagen = (Image)e.Item.FindControl("ImageImagen");

                string imreBase64Data = Convert.ToBase64String(imageDto.file);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                imageImagen.ImageUrl = imgDataURL;


                DataList imageLabelsDataList = (DataList)e.Item.FindControl("ImageTagsDataList");
                imageLabelsDataList.DataSource = imageDto.Tags;
                imageLabelsDataList.DataBind();
            }
        }

        protected void ImageTagsDataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListItemType.Item) || e.Item.ItemType.Equals(ListItemType.AlternatingItem))
            {
                TagDTO tagOutput = (TagDTO)e.Item.DataItem;

                Label label = (Label)e.Item.FindControl("Tag");
                label.Text = tagOutput.Name;
            }
        }

        protected void PreviousImageLinkButton_Click(object sender, EventArgs e)
        {
            CurrentImagePage--;
            LoadImages();
        }

        protected void NextImageLinkButton_Click(object sender, EventArgs e)
        {
            CurrentImagePage++;
            LoadImages();
        }

        #region Private functions

        private void LoadTags()
        {
            IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            IList<TagDTO> tagsDto = imageService.FindTagsOnImages(TagPageSize);

            if (tagsDto.Count > 0)
            {
                maxImage = tagsDto[0].Uses;
            }

            TagsDataList.DataSource = tagsDto;
            TagsDataList.DataBind();
        }

        private void LoadImages()
        {
            if (!(ViewState["tag"] is null))
            {
                IIoCManager iocManager = (IIoCManager)Context.Application["ManagerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                ImagePageable imagePageable = imageService.FindImagesByTagPageable(ImagePageSize, CurrentImagePage, (long)ViewState["tag"]);

                ImagesDataList.DataSource = imagePageable.ImageWithTagsDTO;
                ImagesDataList.DataBind();

                PreviousImageLinkButton.Enabled = imagePageable.HasPrevious;
                NextImageLinkButton.Enabled = imagePageable.HasNext;
                ImagePageTag.Text = Convert.ToString(CurrentImagePage + 1);

                InfoLabel.Visible = false;
                EmptyData.Visible = imagePageable.ImageWithTagsDTO.Count.Equals(0);
            }
            else
            {
                InfoLabel.Visible = true;
                EmptyData.Visible = false;
            }
        }

        private short CurrentImagePage
        {
            get
            {
                if (ViewState["imagePage"] is null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt16(ViewState["imagePage"]);
                }
            }
            set
            {
                ViewState["imagePage"] = value;
            }
        }

        private float ScaleBetween(float unscaledNum, float minAllowed, float maxAllowed, float min, float max)
        {
            return (maxAllowed - minAllowed) * (unscaledNum - min) / (max - min) + minAllowed;
        }

        #endregion Private functions
    }
}