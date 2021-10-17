using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    class ImageService : IImageService
    {
        public ImageService()
        {
        }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Transactional]
        public Image postImage(Image image)
        {
            ImageDao.Create(image);
            return image;
        }

        [Transactional]
        public ImageBlock searchImages(string keywords, string category)
        {

            List<Image> images = ImageDao.FindByKeywordsAndCategory(keywords, category);
            bool existMoreImages = false;

            return new ImageBlock(images, existMoreImages);
        }
    }
}
