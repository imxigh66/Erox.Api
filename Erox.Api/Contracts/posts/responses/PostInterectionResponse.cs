using Erox.Domain.Aggregates.PostAggregate;

namespace Erox.Api.Contracts.posts.responses
{
    public class PostInterectionResponse
    {
        public Guid InteractionId { get; set; }
        public string Type { get; set; }
        public InteractionUser Author { get; set; }
    }
}
