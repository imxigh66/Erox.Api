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
    public class DeletePost:IRequest<OperationResult<Post>>
    {
       public Guid PostId {  get; set; }
        public Guid UserProfileId { get; set; }
    }
}
