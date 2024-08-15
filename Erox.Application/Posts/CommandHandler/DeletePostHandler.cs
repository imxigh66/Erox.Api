using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts.Commands;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.CommandHandler
{
    public class DeletePostHandler : IRequestHandler<DeletePost, OperationResult<Post>>
    {
        private readonly DataContext _ctx;
        public DeletePostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Post>> Handle(DeletePost request, CancellationToken cancellationToken)
        {
           var result = new OperationResult<Post>();
            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p=>p.PostId==request.PostId);
                if (post is null)
                {

                    result.AddError(ErrorCode.NotFound, string.Format(PostsErrorMessage.PostNotFound,request.PostId));
                    return result;
                }

                if(post.UserProfileId!=request.UserProfileId)
                {
                    result.AddError(ErrorCode.PostDeleteNotPossible, PostsErrorMessage.PostdeleteNotPossible);
                    return result;
                }
                _ctx.Posts.Remove(post);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = post;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
