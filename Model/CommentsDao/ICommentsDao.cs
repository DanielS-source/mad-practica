using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsDao
{
    interface ICommentsDao : IGenericDao<Comments, Int64>
{       /// <summary>
        /// Puts a user comment on a given image.
        /// </summary>
        /// <param name="imgId">the image identifier</param>
        /// <param name="usrId">the user identifier</param>
        void AddComment(long imgId, long usrId);

        /// <summary>
        /// Modify a selected comment.
        /// </summary>
        /// <param name="usrId">the user identifier</param>
        /// <param name="comment">the edited comment</param>
        void ModifyComment(long usrId, String comment);

        /// <summary>
        /// Delete a selected comment
        /// </summary>
        /// <param name="comId">the comment identifier</param>
        /// <returns>the list of accounts</returns>
        void DeleteComment(long comId);
    }
}
