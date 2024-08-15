using Erox.Application.Models;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;


namespace Erox.Application.Posts.Commands
{
    public class CreatePost:IRequest<OperationResult<Post>>
    {
        public Guid UserProfileId { get; set; }
        public string TextContent { get; set; }
    }
}
