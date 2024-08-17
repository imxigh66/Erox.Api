using AutoMapper;
using Erox.Api.Contracts.product.requests;
using Erox.Api.Contracts.product.responses;
using Erox.Api.Filters;
using Erox.Application.Products.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Erox.Api.ApiRoutes;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ProductSizeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductSizeController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [Route(ApiRoutes.Product.ProductSize)]
        [ValidateGuid("productId")]
        public async Task<IActionResult> CreateSize(string productId, [FromBody] ProductSizeCreate newSize, CancellationToken cancellationToken)
        {

            var command = new CreateProductSize()
            {
                ProductId = Guid.Parse(productId),
                Sizes=newSize.Size

            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ProductSizeResponse[]>(result.PayLoad);

            return Ok(mapped);
        }
    }
}
