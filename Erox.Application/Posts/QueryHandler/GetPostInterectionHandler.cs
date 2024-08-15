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
    public class GetPostInterectionHandler : IRequestHandler<GetPostInteractions, OperationResult<List<PostInterection>>>
    {
        private readonly DataContext _ctx;
        public GetPostInterectionHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<List<PostInterection>>> Handle(GetPostInteractions request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<PostInterection>>();
            try
            {
                var post= await _ctx.Posts
                    .Include(p=>p.Interection)
                    .ThenInclude(i=>i.UsersProfile)
                    .FirstOrDefaultAsync(p=>p.PostId == request.PostId,cancellationToken);

                if (post == null)
                {
                    result.AddError(Enums.ErrorCode.NotFound, PostsErrorMessage.PostNotFound);
                    return result;
                }

                result.PayLoad=post.Interection.ToList();
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
