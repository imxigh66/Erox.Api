using AutoMapper;
using Erox.Api.Contracts.posts.requests;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.product.requests;
using Erox.Api.Contracts.product.responses;
using Erox.Api.Filters;
using Erox.Application.Posts.Commands;
using Erox.Application.Posts.Queries;
using Erox.Application.Products.Command;
using Erox.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{

    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.Product.getById)]
        [ValidateGuid("id")]
        public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            var productId = Guid.Parse(id);
            var query = new GetProductById() { ProductId=productId };
            var result = await _mediator.Send(query, cancellationToken);
            var mapped = _mapper.Map<ProductResponce>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);

        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] ProductCreate newProduct, CancellationToken cancellationToken)
        {
            
            var command = new CreateProduct
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price=newProduct.Price,
                DiscountPrice=newProduct.DiscountPrice,
                Color=newProduct.Color,
                Category=newProduct.Category,
                Season=newProduct.Season,
                Size=newProduct.Size,
                Code=newProduct.Code,
                Image   =newProduct.Image,
                
            };
            var result = await _mediator.Send(command, cancellationToken);
            

            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<ProductResponce>(result.PayLoad);
            return Ok(mapped);
        }
    }
}
