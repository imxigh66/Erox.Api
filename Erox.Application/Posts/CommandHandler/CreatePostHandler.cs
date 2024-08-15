using Erox.Application.Models;
using Erox.Application.Posts.Commands;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Posts.CommandHandler
{
    public class CreatePostHandler : IRequestHandler<CreatePost, OperationResult<Post>>
    {
        private readonly DataContext _ctx;
        public CreatePostHandler(DataContext ctx)
        {
            _ctx = ctx; 
        }
        public async Task<OperationResult<Post>> Handle(CreatePost request, CancellationToken cancellationToken)
        {
            var result= new OperationResult<Post>();    
            try
            {
                var post = Post.CreatePost(request.UserProfileId, request.TextContent);
                _ctx.Posts.Add(post);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = post;
            }
            catch (PostNotValidException e)
            {

                
                e.ValidationErrors.ForEach(er =>
                {
                    result.AddError(Enums.ErrorCode.ValidationError, er);
                });
            }
            catch(Exception ex) 
            {
                result.AddUnknownError( ex.Message);
            }
            return result;
        }
    }
}
