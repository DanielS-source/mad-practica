﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class SearchImageDTO
    {
        public SearchImageDTO(Image image, string imgUser, string imgCategory, byte[] img, List<Comments> imgComments)

        {
            usrId = image.usrId;
            title = image.title;
            description = image.description;
            dateImg = image.dateImg;
            f = image.f;
            t = image.t;
            ISO = image.ISO;
            wb = image.wb;
            likes = image.likes;

            category = imgCategory;
            username = imgUser;
            file = img;
            comments = imgComments;
        }
        public long usrId { get; set; }
        public string username { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dateImg { get; set; }
        public string f { get; set; }
        public string t { get; set; }
        public string ISO { get; set; }
        public string wb { get; set; }
        public string category { get; set; }
        public long likes { get; set; }
        public byte[] file { get; set; }
        public string imageSrc { get; set; }
        public List<Comments> comments { get; set; }
    }
}