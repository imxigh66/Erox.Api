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
    public class AddInteraction:IRequest<OperationResult<PostInterection>>
    {

        public Guid PostId { get; set; }
        public Guid UserProfileId { get; set; }
        public Interectiontype Type { get; set; }
    }
}
