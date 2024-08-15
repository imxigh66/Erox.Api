using Erox.Domain.Aggregates.PostAggregate;
using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.posts.requests
{
    public class PostInteractionCreate
    {
        [Required]
        public Interectiontype Type { get; set; }
    }
}
