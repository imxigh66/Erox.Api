using Erox.Domain.Aggregates.UsersProfiles;

namespace Erox.Api.Contracts.posts.responses
{
    public class PostResponce
    {
        public Guid PostId { get;  set; }
        public Guid UserProfileId { get;  set; }
        public string TextContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
