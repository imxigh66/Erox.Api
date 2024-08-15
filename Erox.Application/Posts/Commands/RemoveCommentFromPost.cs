using Erox.Application.Models;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.Commands
{
    public class RemoveCommentFromPost:IRequest<OperationResult<PostComment>>
    {

        public Guid UserProfileId { get; set; }
        public Guid Postid { get; set; }
        public Guid CommentId { get; set; }
    }
}
