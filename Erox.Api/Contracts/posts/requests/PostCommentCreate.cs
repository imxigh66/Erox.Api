using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.posts.requests
{
    public class PostCommentCreate
    {
        [Required]
        public string Text { get; set; }
     
    }
}
