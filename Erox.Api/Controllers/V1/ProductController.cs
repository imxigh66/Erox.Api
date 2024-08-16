using AutoMapper;
using Erox.Api.Contracts.posts.requests;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.product.requests;
using Erox.Api.Contracts.product.responses;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Posts.Commands;
using Erox.Application.Posts.Queries;
using Erox.Application.Products.Command;
using Erox.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProducts(), cancellationToken);
            var mapped = _mapper.Map<List<ProductResponce>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.Product.getById)]
        [ValidateGuid("id")]
        [ValidateModel]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreate updatedProduct, string id, CancellationToken cancellationToken)
        {
            

            var command = new UpdateProduct()
            {
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Size = updatedProduct.Size,
                Code = updatedProduct.Code,
                Image = updatedProduct.Image,
                Category= updatedProduct.Category,
                Color = updatedProduct.Color,
                Season = updatedProduct.Season,
                Price = updatedProduct.Price,
                DiscountPrice = updatedProduct.DiscountPrice,


                ProductId = Guid.Parse(id),
                

            };
            var result = await _mediator.Send(command, cancellationToken);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.Product.getById)]
        [ValidateGuid("id")]
        public async Task<IActionResult> DeletePost(string id, CancellationToken cancellationToken)
        {
           
            var command = new DeleteProduct() { ProductId = Guid.Parse(id)};
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpPost]
        [Route(ApiRoutes.Product.ProductReview)]
        [ValidateGuid("productId")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddProductReview(string productId, [FromBody] ProductReviewCreate review, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();



            var command = new AddProductReview()
            {
                ProductId = Guid.Parse(productId),
               
                Rating = review.Rating,
                IsApproved = review.IsApproved,
                Text = review.Text,
                

                
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var newReview = _mapper.Map<ProductReviewResponse>(result.PayLoad);

            return Ok(new
            {
                UserProfileId = userProfileId,
                Review = newReview
            });
        }
    }
}
