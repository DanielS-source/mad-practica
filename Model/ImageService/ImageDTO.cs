using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageDTO
    {
        public ImageDTO(Image image, string categoryName)
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
            catId = image.catId;
            category = categoryName;
        }

        public ImageDTO() {}

        public long usrId { get;  set; }
        public string pathImg { get;  set; }
        public string title { get;  set; }
        public string description { get;  set; }
        public DateTime dateImg { get;  set; }
        public string f { get;  set; }
        public string t { get;  set; }
        public string ISO { get;  set; }
        public string wb { get;  set; }
        public long catId { get;  set; }
        public string category { get;  set; }
        public byte[] file { get; set; }



    }
}
