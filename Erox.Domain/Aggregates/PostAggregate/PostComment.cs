using Erox.Domain.Exeptions;
using Erox.Domain.Validators.PostValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.PostAggregate
{
    public class PostComment
    {
        public PostComment()
        {
            
        }
        public Guid CommentId { get; private set; }
        public Guid Postid { get; private set; }
        public string Text { get; private set; }    
        public Guid UserProfieId { get; private set; }
        public DateTime CreatedData { get; private set; }
        public DateTime LastModified { get; private set;}

        public static PostComment CreatePostComment(Guid postid,string text,Guid userProfileId)
        {
            var validator = new PostCommentValidator();
            var objectToValidate= new PostComment
            {
                Postid = postid,
                Text = text,
                UserProfieId = userProfileId,
                CreatedData = DateTime.Now,
                LastModified = DateTime.Now,
            };
            var validationResult= validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exeption = new PostCommentNotValidException("Post comment is not valid");
            validationResult.Errors.ForEach(vr=>exeption.ValidationErrors.Add(vr.ErrorMessage));
            throw exeption;
        }

        //Factories
        /// <summary>
        /// Creates a post comment
        /// </summary>
        /// <param name="postId">The ID of the post to which the comment belongs</param>
        /// <param name="text">Text content of the comment</param>
        /// <param name="userProfileId">The ID of the user who created the comment</param>
        /// <returns><see cref="PostComment"/></returns>
        /// <exception cref="PostCommentNotValidException">Thrown if the data provided for the post comment
        /// is not valid</exception>
        public void UpdateCommenttext(string newText)
        {
            Text = newText;
            LastModified = DateTime.UtcNow;
        }
    }
}
