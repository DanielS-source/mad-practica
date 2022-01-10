using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable]
    public class ImageWithTagsDto
    {
        public ImageWithTagsDto(Image image, string login, IList<TagDTO> tags)
        {
            imgId = image.imgId;
            usrId = image.usrId;
            LoginName = login;
            pathImg = image.pathImg;
            title = image.title;
            description = image.description;
            dateImg = image.dateImg;
            f = image.f;
            t = image.t;
            ISO = image.ISO;
            wb = image.wb;
            likes = image.LikedBy.Count;
            Tags = tags;
        }

        public ImageWithTagsDto(Image image, IList<TagDTO> tags, string imgUser, string imgCategory, byte[] img, List<Comments> imgComments)
        {
            imgId = image.imgId;
            usrId = image.usrId;
            title = image.title;
            description = image.description;
            dateImg = image.dateImg;
            f = image.f;
            t = image.t;
            ISO = image.ISO;
            wb = image.wb;
            likes = image.LikedBy.Count;
            Tags = tags;

            category = imgCategory;
            LoginName = imgUser;
            file = img;
            comments = imgComments;
        }

        public ImageWithTagsDto() { }

        public long imgId { get; set; }
        public long usrId { get; set; }
        public string LoginName { get; set; }
        public string pathImg { get; set; }
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
        public IList<TagDTO> Tags { get; private set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int multiplier = 31;
                int hash = GetType().GetHashCode();

                hash = hash * multiplier + imgId.GetHashCode();
                hash = hash * multiplier + usrId.GetHashCode();
                hash = hash * multiplier + (LoginName == null ? 0 : LoginName.GetHashCode());
                hash = hash * multiplier + (pathImg == null ? 0 : pathImg.GetHashCode());
                hash = hash * multiplier + (title == null ? 0 : title.GetHashCode());
                hash = hash * multiplier + (description == null ? 0 : description.GetHashCode());
                hash = hash * multiplier + dateImg.GetHashCode();
                hash = hash * multiplier + (f == null ? 0 : f.GetHashCode());
                hash = hash * multiplier + (t == null ? 0 : t.GetHashCode());
                hash = hash * multiplier + (ISO == null ? 0 : ISO.GetHashCode());
                hash = hash * multiplier + (wb == null ? 0 : wb.GetHashCode());
                hash = hash * multiplier + Tags.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type?

            ImageWithTagsDto target = (ImageWithTagsDto)obj;

            return (this.imgId == target.imgId)
                && (this.usrId == target.usrId)
                && (this.LoginName == target.LoginName)
                && (this.pathImg == target.pathImg)
                && (this.title == target.title)
                && (this.description == target.description)
                && (this.dateImg == target.dateImg)
                && (this.f == target.f)
                && (this.t == target.t)
                && (this.ISO == target.ISO)
                && (this.wb == target.wb)
                && (this.Tags == target.Tags);
        }
    }
}
