﻿using Erox.Application.Models;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.Queries
{
    public class GetPostComments:IRequest<OperationResult<List<PostComment>>>
    {
        public Guid PostId { get; set; }
    }
}
