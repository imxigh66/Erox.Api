using AutoMapper;
using Erox.Api.Contracts.posts.requests;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.product.requests;
using Erox.Api.Contracts.product.responses;
using Erox.Api.Extentions;
using Erox.Api.Filters;
using Erox.Application.Orders.Queries;
using Erox.Application.Posts.Commands;
using Erox.Application.Posts.Queries;
using Erox.Application.Products.Command;
using Erox.Application.Products.Queries;
using Erox.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Erox.Api.ApiRoutes;

namespace Erox.Api.Controllers.V1
{

    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IMapper mapper, IMediator mediator, ILogger<ProductController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;   
        }

        //[HttpGet]
        //[Route(ApiRoutes.Product.getById)]
        //[ValidateGuid("id")]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
        //{
        //    var productId = Guid.Parse(id);
        //    var query = new GetProductById() { ProductId=productId };
        //    var result = await _mediator.Send(query, cancellationToken);
        //    var mapped = _mapper.Map<ProductResponce>(result.PayLoad);

        //    return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);

        //}
        [HttpGet]
        [Route("GetProductsByFilters")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByFilter(string? id,Guid? categoryId,string? color,string? season,string? code,decimal? price, CancellationToken cancellationToken)
        {
            Guid? productId = null;

            if (!string.IsNullOrEmpty(id))
            {
                if (Guid.TryParse(id, out var parsedId))
                {
                    productId = parsedId;
                }
                else
                {
                    return BadRequest("Invalid ID format.");
                }
            }

            var query = new GetProductByFilter() { 
                CategoryId=categoryId,
                Color=color,
                Season=season,
                Code=code,
                Price=price,
                ProductId=productId
            };
            var result = await _mediator.Send(query, cancellationToken);
            if (result.PayLoad == null || result.PayLoad.Length == 0)
            {
                return NotFound("No products found.");
            }

            // Логирование для отладки
            _logger.LogInformation($"Количество продуктов: {result.PayLoad.Length}");
            var mapped = _mapper.Map<ProductResponce[]>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);

        }

        [HttpPost]
        [ValidateModel]
        [Route("CreatePost")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreate newProduct, CancellationToken cancellationToken)
        {
            
            var command = new CreateProduct
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price=newProduct.Price,
                DiscountPrice=newProduct.DiscountPrice,
                CategoryId=newProduct.CategoryId,
                Color=newProduct.Color,
                Season=newProduct.Season,
                Code=newProduct.Code,
                Image   =newProduct.Image,
                
            };
            var result = await _mediator.Send(command, cancellationToken);
            

            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<ProductResponce>(result.PayLoad);
            return Ok(mapped);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [AllowAnonymous]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductCreate updatedProduct, string id, CancellationToken cancellationToken)
        {
            

            var command = new UpdateProduct()
            {
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Code = updatedProduct.Code,
                Image = updatedProduct.Image,
                CategoryId= updatedProduct.CategoryId,
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
        {
           
            var command = new DeleteProduct() { ProductId = Guid.Parse(id)};
            var result = await _mediator.Send(command, cancellationToken);
            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }

        [HttpPost]
        [Route(ApiRoutes.Product.ProductReview)]
        [ValidateGuid("productId")]
        [ValidateModel]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="AppUser")]
        public async Task<IActionResult> AddProductReview(string productId, [FromBody] ProductReviewCreate review, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();



            var command = new AddProductReview()
            {
                ProductId = Guid.Parse(productId),
               
                Rating = review.Rating,
                IsApproved = review.IsApproved,
                Text = review.Text,
                UserProfileId = userProfileId

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


        [HttpDelete]
        [Route(ApiRoutes.Product.ReviewById)]
        [ValidateGuid("productId", "reviewId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> RemoveReviewFromProduct(string productId, string reviewId, CancellationToken cancellationToken)
        {
            var userProfileId = HttpContext.GetUserProfileIdClaimValue();
            var productGuid = Guid.Parse(productId);
            var reviewGuid = Guid.Parse(reviewId);
            var command = new DeleteReview
            {
                UserProfileId = userProfileId,
                ProductId=productGuid,
                ReviewId=reviewGuid
            };
            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) { return HandleErrorResponse(result.Errors); }
            return NoContent();
        }



        [HttpGet]
        [Route(ApiRoutes.Product.ProductReview)]
        [AllowAnonymous]
        public async Task<IActionResult> GetReviewById(string productId, CancellationToken cancellationToken)
        {
            var query = new GetReviewByProductId() { ProductId = Guid.Parse(productId) };
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) HandleErrorResponse(result.Errors);

            var reviews = _mapper.Map<List<ProductReviewResponse>>(result.PayLoad);
            return Ok(reviews);
        }

        [HttpPost]
        [Route("Addcategory")]
        public async Task<IActionResult> AddCategory(SexEnum sex,CancellationToken cancellationToken)
        {
            var command = new AddCategory()
            {
                Sex = sex

            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            

            return Ok();
        }

    }
}
