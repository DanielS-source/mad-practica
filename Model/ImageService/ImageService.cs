using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System.Runtime.Caching;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        private MemoryCache ImageCache = new MemoryCache("MemoryCache");
        private List<string> keys = new List<string>();

        public ImageService()
        {
        }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Transactional]
        public Image PostImage(Image image)
        {
            ImageDao.Create(image);
            return image;
        }

        [Transactional]
        public ImageBlock SearchImages(string keywords, string category, int startIndex, int count)
        {
            //Checks if its in the cache
            List<Image> images = (List<Image>)ImageCache[keywords + category];

            //If its not found in the cache
            if (images == null) {

                images = ImageDao.FindByKeywordsAndCategory(keywords, category, startIndex, count);
                keys.Add(keywords + category);

                if (keys.Count > 5)
                {
                    //Removes last item if cache size exceeds 5
                    ImageCache.Remove(keys.First());
                    keys.Remove(keys.First());
                }
                //Adds entry to cache
                ImageCache[keywords + category] = images;

            }

            bool existMoreImages = (images.Count == count);

            return new ImageBlock(images, existMoreImages);
        }

        public ImageBlock SearchFollowedImages(long usrId, int startIndex, int count)
        {

            List<Image> images = ImageDao.FindByFollowed(usrId, startIndex, count);

            bool existMoreImages = (images.Count == count);

            return new ImageBlock(images, existMoreImages);
        }

        public void DeleteImage(long imageId)
        {
            ImageDao.Remove(imageId);
        }
    }
}
