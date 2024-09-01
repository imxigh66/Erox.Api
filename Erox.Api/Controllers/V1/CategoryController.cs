using AutoMapper;
using Erox.Api.Contracts.product.requests;
using Erox.Api.Contracts.product.responses;
using Erox.Application.Products.Command;
using Erox.Application.Products.Queries;
using Erox.Domain.Enumerations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Erox.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
      
        public CategoryController(IMapper mapper, IMediator mediator, ILogger<ProductController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
          
        }
        [HttpPost]
        [Route("Addcategory")]
        public async Task<IActionResult> AddCategory(CategoryRequest category, CancellationToken cancellationToken)
        {
            var command = new AddCategory()
            {
                Sex = category.Sex,
                CategoryTranslations=category.CategoryTranslation.Select(s => new ProductTranslationCreateCommand { LanguageCode = s.LanguageCode, Title = s.Title }).ToArray(),


            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);



            return Ok();
        }

        [HttpGet]
        [Route("Getcategory")]
        public async Task<IActionResult> GetCategory(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCategories(), cancellationToken);
            var mapped = _mapper.Map<List<CategoryResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }
    }
}
