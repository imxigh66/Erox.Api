using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.wishlist.response;
using Erox.Api.Filters;
using Erox.Application.Cards.Command;
using Erox.Application.Wishlists.Command;
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
    public class CardController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public CardController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddToCard([FromBody] AddToCard command, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(command, cancellationToken);


            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<CardProductResponse>(result.PayLoad);
            return Ok(mapped);
        }
    }
}
