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
    public class GetAllPostsHandler : IRequestHandler<GetAllPosts, OperationResult<List<Post>>>
    {
        private readonly DataContext _ctx;
        public GetAllPostsHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<List<Post>>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Post>>();
            try
            {
                
                var posts = await _ctx.Posts.ToListAsync();
                result.PayLoad = posts;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
                
            }
            return result;
        }
    }
}
