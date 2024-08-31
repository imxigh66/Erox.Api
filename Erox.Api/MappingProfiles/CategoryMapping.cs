using AutoMapper;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.ProductAggregate;

namespace Erox.Api.MappingProfiles
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryResponse>();

        }
    }
}
