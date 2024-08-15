using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.posts.requests
{
    public class PostUpdate
    {
        [Required]
        public string Text {  get; set; }
    }
}
