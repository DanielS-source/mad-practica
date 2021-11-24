using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    class ImageDTO
    {
        public ImageDTO(Image image, String categoryName)
        {
            usrId = image.usrId;
            pathImg = image.pathImg;
            title = image.title;
            description = image.description;
            dateImg = image.dateImg;
            f = image.f;
            t = image.t;
            ISO = image.ISO;
            wb = image.wb;
            category = categoryName;
        }

        public long usrId { get; private set; }
        public string pathImg { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public DateTime dateImg { get; private set; }
        public string f { get; private set; }
        public string t { get; private set; }
        public string ISO { get; private set; }
        public string wb { get; private set; }
        public string category { get; private set; }



    }
}
