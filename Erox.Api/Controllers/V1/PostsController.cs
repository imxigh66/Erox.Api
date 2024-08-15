using AutoMapper;
using Erox.Api.Contracts.common;
using Erox.Api.Contracts.posts.requests;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Posts.Commands;
using Erox.Application.Posts.Queries;
using Erox.Domain.Aggregates.PostAggregate;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostsController(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllPosts(), cancellationToken);
            var mapped = _mapper.Map<List<PostResponce>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }


        [HttpGet]
        [Route(ApiRoutes.Post.getById)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var postId = Guid.Parse(id);
            var query = new GetPostById() { PostId = postId };
            var result = await _mediator.Send(query, cancellationToken);
            var mapped = _mapper.Map<PostResponce>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate newPost, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new CreatePost()
            {
                UserProfileId = userProfileId, 
                TextContent = newPost.TextContent
            };

            var result = await _mediator.Send(command, cancellationToken);
            
            var mapped=_mapper.Map<PostResponce>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors)
                : CreatedAtAction(nameof(GetById), new { id = result.PayLoad.UserProfileId ,mapped});
        }

        [HttpPatch]
        [Route(ApiRoutes.Post.getById)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePostText([FromBody] PostUpdate updatedPost, string id, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();

            var command = new UpdatePostText()
            {
                NewText = updatedPost.Text,
                PostId = Guid.Parse(id),
                UserProfileId=userProfileId
               
            };
            var result = await _mediator.Send(command,cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Post.getById)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id, CancellationToken cancellationToken)
        {
            var userProfileId= HttpContext.GetUserProfileIdClaimValue();
            var command = new DeletePost() { PostId= Guid.Parse(id) , UserProfileId=userProfileId};  
            var result=await _mediator.Send(command,cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : NotFound();
        }


        [HttpGet]
        [Route(ApiRoutes.Post.PostComments)]
        public async Task<IActionResult> GetCommentsByPostId(string postId, CancellationToken cancellationToken)
        {
            var query = new GetPostComments() { PostId = Guid.Parse(postId) };
            var result = await _mediator.Send(query,cancellationToken);

            if (result.IsError) HandleErrorResponse(result.Errors);

            var comments = _mapper.Map<List<PostCommentResponse>>(result.PayLoad);
            return Ok(comments);
        }

        [HttpPost]
        [Route(ApiRoutes.Post.PostComments)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddCommentToPost(string postId, [FromBody] PostCommentCreate comment, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
           
          

            var command = new AddPostComment()
            {
                PostId = Guid.Parse(postId),
                UserProfileid = userProfileId,
                TextComment = comment.Text
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newComment = _mapper.Map<PostCommentResponse>(result.PayLoad);

            return Ok(newComment);
        }

        [HttpDelete]
        [Route(ApiRoutes.Post.CommentById)]
        [ValidateGuid("postId","commentId")]
        public async Task<IActionResult> RemoveCommentFromPost(string postId,string commnetId,CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var postGuid=Guid.Parse(postId);
            var commentId=Guid.Parse(commnetId);
            var command = new RemoveCommentFromPost
            {
                UserProfileId= userProfileId,
                CommentId= commentId,
                Postid= postGuid
            };
            var result=await _mediator.Send(command,cancellationToken);
            if (result.IsError) { return HandleErrorResponse(result.Errors); }
            return NoContent();
        }
        
        [HttpPut]
        [Route(ApiRoutes.Post.CommentById)]
        [ValidateGuid("postId", "commentId")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCommentText(string postId,string commentId,PostCommentUpdate updatedComment,CancellationToken cancellationToken)
        {
            var userProfileId=HttpContext.GetUserProfileIdClaimValue();
            var postGuid = Guid.Parse(postId);
            var commentGuid = Guid.Parse(commentId);

            var command = new UpdatePostComment
            {
                UserProfileId = userProfileId,
                PostId = postGuid,
                CommentId = commentGuid,
                Updatedtext=updatedComment.Text
            };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) { return HandleErrorResponse(result.Errors);}


            return NoContent();
        }

        [HttpGet]
        [Route(ApiRoutes.Post.PostInteractions)]
        [ValidateGuid("postId")]
        public async Task<IActionResult> GetPostInterections(string postId,CancellationToken cancellationToken)
        {
            var postGuid= Guid.Parse(postId);
            var query = new GetPostInteractions { PostId = postGuid };
            var result = await _mediator.Send(query, cancellationToken);

            if(result.IsError) HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<List<PostInterectionResponse>>(result.PayLoad);
            return Ok(mapped);
        }


        [HttpPost]
        [Route(ApiRoutes.Post.PostInteractions)]
        [ValidateGuid("postId")]
        [ValidateModel]
        public async Task<IActionResult> AddPostInteraction(string postId,PostInteractionCreate interaction,CancellationToken cancellationToken)
        {
            var postGuid=Guid.Parse(postId);
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var command = new AddInteraction
            {
                PostId = postGuid,
                UserProfileId = userProfileId,
                Type = interaction.Type,
            };

            var result = await _mediator.Send(command,cancellationToken);
            if (result.IsError) HandleErrorResponse(result.Errors);
            var mapped=_mapper.Map<PostInterection>(result.PayLoad);
            return Ok(mapped);

        }


        [HttpDelete]
        [Route(ApiRoutes.Post.InterectionById)]
        [ValidateGuid("postid","interactionId")]
        public async  Task<IActionResult> RemovePostInterection(string postId,string interactionId,CancellationToken cancellationToken)
        {
            var postGuid=Guid.Parse(postId);
            var interactionGuid = Guid.Parse(interactionId);
            var userProfileGuid=HttpContext.GetUserProfileIdClaimValue();
            var command = new RemovePostInteraction
            {
                PostId = postGuid,
                InteractionId = interactionGuid,
                UserProfileId = userProfileGuid

            };

            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<PostInterection>(result.PayLoad);
            return Ok(mapped);

        }


        
    }


}
