using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.posts.requests
{
    public class PostCommentUpdate
    {
        [Required]
        public string Text { get; set; }
    }
}
