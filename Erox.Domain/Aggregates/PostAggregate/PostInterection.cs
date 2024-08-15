using Erox.Domain.Aggregates.UsersProfiles;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.PostAggregate
{
    public class PostInterection
    {
        public PostInterection()
        {
            
        }
        public Guid InterectionId { get; private set; }
        public Guid PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public UserProfiles UsersProfile { get; private set; }
        public Interectiontype InterectionType { get; private set; }

        public static PostInterection CreatePostImteraction(Guid postid,Guid userProfileId, Interectiontype type)
        {
            return new PostInterection
            {
                PostId = postid,
                UserProfileId = userProfileId,
                InterectionType = type,

            };

        }
    }
}
