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
    public class AddPostComment:IRequest<OperationResult<PostComment>>
    {
        public Guid PostId { get; set; }    
        public Guid UserProfileid { get; set; }
        public string TextComment { get; set; }
    }
}
