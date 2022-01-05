using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using Es.Udc.DotNet.PracticaMaD.Web.Http.Session;
using System;

namespace Web.Pages
{
    public partial class ImageDetail : CulturePage
    {

        protected SearchImageDTO image;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.image = new SearchImageDTO(null, null, null, null, null);
        }
    }
}