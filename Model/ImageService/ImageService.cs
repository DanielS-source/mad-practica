using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;

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
        public ImageBlock searchImages(string keywords, string category, int startIndex, int count)
        {

            List<Image> images = ImageDao.FindByKeywordsAndCategory(keywords, category, startIndex, count);

            bool existMoreImages = (images.Count == count + 1);

            return new ImageBlock(images, existMoreImages);
        }
    }
}
