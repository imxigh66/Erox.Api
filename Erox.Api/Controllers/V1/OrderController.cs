using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.orders.request;
using Erox.Api.Contracts.orders.response;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Cards.Command;
using Erox.Application.Cards.Queries;
using Erox.Application.Orders.Command;
using Erox.Application.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddToOrder([FromBody] CreateOrderRequest req, CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserProfileIdClaimValue();
            var command = new CreateOrder() {
                UserId= userId,
                Status=req.Status.ToString(),
                Sum=req.Sum,
                Items=req.Items,
                PaymentMethod=req.PaymentMethod.ToString(),
                ShippingMethod=req.ShippingMethod.ToString(),
                Address=req.Address,
            };
            var result = await _mediator.Send(command, cancellationToken);
            

            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<CreateOrderResponse>(result.PayLoad);
            return Ok(mapped);
        }

        [HttpGet]
        
        public async Task<IActionResult> GetOrderByFilters(Guid? orderId, [FromQuery] string? status, [FromQuery] DateTime? createdDate,CancellationToken cancellationToken)
        {
            var userId = HttpContext.GetUserProfileIdClaimValue();

            // потом сделать чтоб админ смог получить заказы и по userid 
            var query = new GetOrder
            {
                UserId = userId,
                OrderId = orderId,
                Status = status,
                CreatedDate = createdDate 
            };
            var result = await _mediator.Send(query, cancellationToken);
            var mapped= _mapper.Map<List<OrderResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
            
        }

        [HttpDelete]
        [Route(ApiRoutes.Order.getByOrder)]
        [ValidateGuid("id")]
        public async Task<IActionResult> RemoveFromCard(string id, CancellationToken cancellationToken)
        {
            
            var command = new DeleteOrder() { OrderId = Guid.Parse(id)};
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError)
            {
                return HandleErrorResponse(result.Errors);
            }

            return NoContent();
        }
    }
}
