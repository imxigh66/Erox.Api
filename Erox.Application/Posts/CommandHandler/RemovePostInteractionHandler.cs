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
    public class RemovePostInteractionHandler : IRequestHandler<RemovePostInteraction, OperationResult<PostInterection>>
    {
        private readonly DataContext _ctx;
        public RemovePostInteractionHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<PostInterection>> Handle(RemovePostInteraction request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<PostInterection>();
            try
            {
                var post = await _ctx.Posts
                    .Include(p => p.Interection)
                    .FirstOrDefaultAsync(p => p.PostId == request.PostId,cancellationToken);

                if(post is null)
                {
                    result.AddError(Enums.ErrorCode.NotFound,string.Format(PostsErrorMessage.PostNotFound, request.PostId));
                    return result;
                }

                var interaction = post.Interection.FirstOrDefault(i=>i.InterectionId == request.InteractionId);
                if(interaction == null)
                {
                    result.AddError(Enums.ErrorCode.NotFound, PostsErrorMessage.PostInteractionNotFound);
                    return result;
                }

                if(interaction.UserProfileId!=request.UserProfileId)
                {
                    result.AddError(Enums.ErrorCode.InteractionRemovalNotAuthorized, PostsErrorMessage.InteractionRemovalNotAuthorized);
                    return result;
                }
                post.RemoveInteraction(interaction);
                _ctx.Posts.Update(post);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = interaction;

            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
