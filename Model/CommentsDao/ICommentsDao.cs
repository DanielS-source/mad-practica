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
        void AddComment(Comments comment);

        /// <summary>
        /// Modify a selected comment.
        /// </summary>
        /// <param name="usrId">the user identifier</param>
        /// <param name="comment">the edited comment</param>
        void ModifyComment(long comId, String comment);

        /// <summary>
        /// Delete a selected comment
        /// </summary>
        /// <param name="comId">the comment identifier</param>
        void DeleteComment(long comId);

        /// <summary>
        /// Finds all the comments related to a given image 
        /// </summary>
        /// <param name="imgId">the image identifier</param>
        /// <returns>the list of comments related to the given image</returns>
        Comments find(long comId);


        /// <summary>
        /// Finds all the comments related to a given image 
        /// </summary>
        /// <param name="imgId">the image identifier</param>
        /// <returns>the list of comments related to the given image</returns>
        List<Comments> findByImage(long imgId);
    }
}
