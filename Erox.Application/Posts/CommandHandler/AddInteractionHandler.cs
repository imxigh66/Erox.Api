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
    public class AddInteractionHandler : IRequestHandler<AddInteraction, OperationResult<PostInterection>>
    {

        private readonly DataContext _ctx;
        public AddInteractionHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<PostInterection>> Handle(AddInteraction request, CancellationToken cancellationToken)
        {
            var result=new OperationResult<PostInterection>();
            try
            {

                var post = await _ctx.Posts.Include(p=>p.Interection).FirstOrDefaultAsync(p=>p.PostId == request.PostId,cancellationToken);

                if(post==null)
                {
                    result.AddError(Enums.ErrorCode.NotFound,PostsErrorMessage.PostNotFound);
                    return result;
                }
                var interaction = PostInterection.CreatePostImteraction(request.PostId,request.UserProfileId,request.Type);

                post.AddInteraction(interaction);
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
