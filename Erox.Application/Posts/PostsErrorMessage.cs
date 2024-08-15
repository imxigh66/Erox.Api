using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts
{
    public  class PostsErrorMessage
    {
        public const string PostNotFound = "No post found with ID {0}";
        public const string PostdeleteNotPossible = "Only the owner of a post can delete it";
        public const string PostUpdateNotPossible = "Post update not possible becouse it's not the post owner that initiates the update";
        public const string PostInteractionNotFound = "InteractionNotFound";
        public const string InteractionRemovalNotAuthorized = "Cannot remove interaction as you are not its author";
        public const string PostCommentNotFound = "Comment not found";
        public const string CommentRemovalNotAuthorized = "Cannot remove cpmment from post as you are not its author";
    }
}
