using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.QueryHandler
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, OperationResult<Post>>
    {
        private readonly DataContext _ctx;
        public GetPostByIdHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();
            var post = await _ctx.Posts
           .FirstOrDefaultAsync(p => p.PostId == request.PostId);

            if (post is null)
            {

                result.AddError(ErrorCode.NotFound, string.Format(PostsErrorMessage.PostNotFound));
                return result;
            }

            result.PayLoad = post;
            return result;
        }
    }
}
