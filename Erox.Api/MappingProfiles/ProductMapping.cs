using AutoMapper;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.ProductAggregate;

namespace Erox.Api.MappingProfiles
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResponce>();
            CreateMap<ProductReview,ProductReviewResponse>();
        }
    }
}
