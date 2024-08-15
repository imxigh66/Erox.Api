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
    public class RemoveCommentFromPostHandler : IRequestHandler<RemoveCommentFromPost, OperationResult<PostComment>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<PostComment> _result;
        public RemoveCommentFromPostHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result= new OperationResult<PostComment>();    
        }
        public async Task<OperationResult<PostComment>> Handle(RemoveCommentFromPost request, CancellationToken cancellationToken)
        {

            var post=await _ctx.Posts.Include(p=>p.Comments).FirstOrDefaultAsync(p=>p.PostId==request.Postid,cancellationToken);
            if(post==null)
            {
                _result.AddError(Enums.ErrorCode.NotFound,PostsErrorMessage.PostNotFound);
                return _result;
            }

            var comment = post.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
            if(comment==null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, PostsErrorMessage.PostCommentNotFound);
                return _result;
            }
            if(comment.UserProfieId!=request.UserProfileId)
            {
                _result.AddError(Enums.ErrorCode.CommentRemovalNotAuthorized,PostsErrorMessage.CommentRemovalNotAuthorized);
                return _result;
            }

            post.ARemovePostComment(comment);
            _ctx.Posts.Update(post);    
            await _ctx.SaveChangesAsync(cancellationToken);
            _result.PayLoad = comment;
            return _result;
        }
    }
}
