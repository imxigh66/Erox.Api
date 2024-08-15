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
    public class UpdatePostComment:IRequest<OperationResult<PostComment>>    
    {
        public Guid UserProfileId { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
        public string Updatedtext { get; set; }
    }
}
