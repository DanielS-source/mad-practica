using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class SearchImageBlock
    {
        public List<SearchImageDTO> Images { get; private set; }
        public bool ExistMoreImages { get; private set; }

        public SearchImageBlock(List<SearchImageDTO> images, bool existMoreImages)
        {
            this.Images = images;
            this.ExistMoreImages = existMoreImages;
        }

    }
}
