using AutoMapper;
using Erox.Api.Contracts.posts.responses;
using Erox.Domain.Aggregates.PostAggregate;

namespace Erox.Api.MappingProfiles
{
    public class PostMapping:Profile
    {
        public PostMapping()
        {
            CreateMap<Post,PostResponce>();
            CreateMap<PostComment, PostCommentResponse>();
        }
    }
}
