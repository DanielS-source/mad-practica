using Es.Udc.DotNet.PracticaMaD.Model.CommentsDao;
using Es.Udc.DotNet.ModelUtil.Transactions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentsService
{
    public interface ICommentsService
    {
        ICommentsDao CommentsDao { set; }

        /// <summary>
        /// Creates a comment.
        /// </summary>
        /// <param name="commentt">The comment.</param>
        /// <returns>The comment created</returns>
        [Transactional]
        Comments AddComment(Comments comment);

        /// <summary>
        /// Removes the comment.
        /// </summary>
        /// <param name="commentId">The comment identifier.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comments DeleteComment(long commentId);

        /// <summary>
        /// Updates the text of the comment.
        /// </summary>
        /// <param name="commentText">The new text of the comment.</param>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        Comments UpdateComment(string commentText);

        /// <summary>
        /// Gets the list of comments associated to an image.
        /// </summary>
        /// <param name="imgId">The image identifier.</param>
        [Transactional]
        List<Comments> ShowComments(long imgId);

    }
}
