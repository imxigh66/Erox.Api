using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts.Commands;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.CommandHandler
{
    public class UpdatePostTextHandler : IRequestHandler<UpdatePostText, OperationResult<Post>>
    {
        private readonly DataContext _ctx;

        public UpdatePostTextHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Post>> Handle(UpdatePostText request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Post>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken: cancellationToken);

                if (post is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(PostsErrorMessage.PostNotFound, request.PostId));
                    
                    return result;
                }

                if(post.UserProfileId!=request.UserProfileId)
                {
                    result.AddError(ErrorCode.PostUpdateNotPossible, PostsErrorMessage.PostUpdateNotPossible);
                    return result;
                }
                post.UpdatePosttext(request.NewText);

                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = post;
                return result;
            }

            catch (PostNotValidException e)
            {

                e.ValidationErrors.ForEach(er =>
                {
                    result.AddError(ErrorCode.ValidationError, er);
                });
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            return result;
        }
    }
}
