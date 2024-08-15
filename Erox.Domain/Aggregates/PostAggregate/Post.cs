using Erox.Domain.Aggregates.UsersProfile;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Exeptions;
using Erox.Domain.Validators.PostValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.PostAggregate
{
    public class Post
    {
        private readonly List<PostComment> _comments=new List<PostComment>();
        private readonly List<PostInterection> _interections=new List<PostInterection>();   
        private Post()
        {
           
        }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfiles UserProfile { get; private set; }
        public string TextContent {  get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set;}
        public IEnumerable<PostComment> Comments { get { return _comments; } }
        public IEnumerable<PostInterection> Interection { get { return _interections; } }

        public static Post CreatePost(Guid userProfileid, string textContent)
        {
            var validator = new PostValidator();
            var objectToValidate = new Post
            {
                UserProfileId = userProfileid,
                TextContent = textContent,
                CreatedDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new PostNotValidException("Post is not valid");
            validationResult.Errors.ForEach(vr => exception.ValidationErrors.Add(vr.ErrorMessage));
            throw exception;
        }

        public void UpdatePosttext(string newText)
        {
            if (string.IsNullOrWhiteSpace(newText))
            {
                var exception = new PostNotValidException("Cannot update post." +
                                                          "Post text is not valid");
                exception.ValidationErrors.Add("The provided text is either null or contains only white space");
                throw exception;
            }
            TextContent = newText;
            LastModified = DateTime.UtcNow;
        }

        public void AddPostComment(PostComment comment)
        {
             _comments.Add(comment);
        }

        public void ARemovePostComment(PostComment toRemove)
        {
            _comments.Remove(toRemove);
        }


        public void UpdatePostComment(Guid postCommentId,string updatedComment)
        {
            var comment =_comments.FirstOrDefault(c=>c.CommentId == postCommentId);
            if(comment !=null && !string.IsNullOrWhiteSpace(updatedComment))
            {
                comment.UpdateCommenttext(updatedComment);
            }
        }

        public void AddInteraction(PostInterection _newInteraction)
        {
            _interections.Add(_newInteraction);
        }

        public void RemoveInteraction(PostInterection toRemove)
        {
            _interections.Remove(toRemove);
        }
    }
}
