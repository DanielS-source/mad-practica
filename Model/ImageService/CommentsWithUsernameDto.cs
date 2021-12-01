using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    [Serializable()]
    public class CommentsWithUsernameDto
    {
        public long comId { get; private set; }

        public long imgId { get; private set; }

        public long usrId { get; private set; }

        public String message { get; private set; }

        public DateTime postDate { get; private set; }

        public String loginName { get; private set; }

        public CommentsWithUsernameDto(long comId, long imgId, long usrId,
            String message, DateTime postDate, String loginName)
        {
            this.comId = comId;
            this.imgId = imgId;
            this.usrId = usrId;
            this.message = message;
            this.postDate = postDate;
            this.loginName = loginName;
        }
        public override bool Equals(object obj) 
        {
            CommentsWithUsernameDto target = (CommentsWithUsernameDto)obj;

            return (this.comId == target.comId
                    && this.imgId == target.imgId
                    && this.usrId == target.usrId
                    && this.message == target.message
                    && this.postDate == target.postDate
                    && this.loginName == target.loginName);
        }

        public override int GetHashCode()
        {
            int hashCode = 512769504;
            hashCode = hashCode * -1521134295 + comId.GetHashCode();
            hashCode = hashCode * -1521134295 + imgId.GetHashCode();
            hashCode = hashCode * -1521134295 + usrId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(message);
            hashCode = hashCode * -1521134295 + postDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(loginName);
            return hashCode;
        }
    }
}
