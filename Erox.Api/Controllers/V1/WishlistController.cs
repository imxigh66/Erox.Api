using AutoMapper;
using Erox.Api.Contracts.product.responses;
using Erox.Api.Contracts.wishlist.response;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Products.Command;
using Erox.Application.Products.Queries;
using Erox.Application.Wishlists.Command;
using Erox.Application.Wishlists.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WishlistController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public WishlistController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddToWishlist([FromBody] AddToWishlist command,CancellationToken cancellationToken)
        {
            
            var result = await _mediator.Send(command, cancellationToken);


            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<AddWishlistResponse>(result.PayLoad);
            return Ok(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist(CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserProfileIdClaimValue();

            // Создаем запрос
            var query = new GetWishlist
            {
                UserId = userId
            };
            var result = await _mediator.Send(query, cancellationToken);
            var mapped = _mapper.Map<List<WishlistProductResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpDelete]
        [Route(ApiRoutes.Product.getById)]
        [ValidateGuid("id")]
        public async Task<IActionResult> RemoveFromWishlist(string id, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserProfileIdClaimValue();
            var command = new RemoveFromWishlist() { ProductId = Guid.Parse(id), UserId = userId };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError)
            {
                return HandleErrorResponse(result.Errors);
            }

            return NoContent();
        }
    }
}
