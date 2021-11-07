﻿using Es.Udc.DotNet.PracticaMaD.Model.ImageDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System.Runtime.Caching;
using Es.Udc.DotNet.PracticaMaD.Model.Cache;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
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
            List<Image> images = new List<Image>();

            //Checks if its in the cache
            if (ImageCache.Exists(keywords + category)) {
                images = ImageCache.Get<List<Image>>(keywords + category);
            }
            else
            {
                images = ImageDao.FindByKeywordsAndCategory(keywords, category, startIndex, count);

                //Adds the results to the cache
                ImageCache.Add(keywords + category, images);
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
