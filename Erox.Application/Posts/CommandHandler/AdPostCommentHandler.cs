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
    public class AdPostCommentHandler : IRequestHandler<AddPostComment, OperationResult<PostComment>>
    {
        private readonly DataContext _ctx;
        public AdPostCommentHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<PostComment>> Handle(AddPostComment request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostComment>();

            try
            {
                var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.PostId == request.PostId,cancellationToken);

                if (post is null)
                {
                    
                    result.AddError(ErrorCode.NotFound,string.Format(PostsErrorMessage.PostNotFound,request.PostId));
                    return result;
                }

                var comment = PostComment.CreatePostComment(request.PostId, request.TextComment, request.UserProfileid);

                post.AddPostComment(comment);

                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = comment;

            }
            catch (PostCommentNotValidException e)
            {

             
                e.ValidationErrors.ForEach(er =>
                {
                 
                    result.AddError(ErrorCode.ValidationError, er);
                });
            }

            catch (Exception e)
            {
                
                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
