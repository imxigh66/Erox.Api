using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.posts.requests
{
    public class PostCreate
    {
        [Required]
        public string TextContent { get; set; }
        
    }
}
