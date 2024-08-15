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
    public class UpdatePostCommentHandler : IRequestHandler<UpdatePostComment, OperationResult<PostComment>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<PostComment> _result;
        public UpdatePostCommentHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result = new OperationResult<PostComment>();
        }
        public async Task<OperationResult<PostComment>> Handle(UpdatePostComment request, CancellationToken cancellationToken)
        {

            var post = await _ctx.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.PostId == request.PostId, cancellationToken);
            if (post == null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, PostsErrorMessage.PostNotFound);
                return _result;
            }

            var comment = post.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
            if (comment == null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, PostsErrorMessage.PostCommentNotFound);
                return _result;
            }
            if (comment.UserProfieId != request.UserProfileId)
            {
                _result.AddError(Enums.ErrorCode.CommentRemovalNotAuthorized, PostsErrorMessage.CommentRemovalNotAuthorized);
                return _result;
            }
            comment.UpdateCommenttext(request.Updatedtext);
            _ctx.Posts.Update(post);
            await _ctx.SaveChangesAsync(cancellationToken);
            return _result;
        }
    }
}
