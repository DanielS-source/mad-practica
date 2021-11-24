using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class CommentsBlock
    {
        public List<CommentsWithUsernameDto> CommentList { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentsBlock(List<CommentsWithUsernameDto> CommentList, bool existMoreComments)
        {
            this.CommentList= CommentList;
            this.ExistMoreComments = existMoreComments;
        }
    }
}
