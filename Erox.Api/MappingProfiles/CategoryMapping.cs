using AutoMapper;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.Translations;

namespace Erox.Api.MappingProfiles
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, CategoryResponse>()
                .ForMember(des => des.Names, opt => opt.MapFrom(src => src.CategoryTranslations));

            CreateMap<CategoryTranslation, ProductTranslationResponse>()
                .ForMember(d => d.LanguageCode, o => o.MapFrom(s => s.Language)); ;
        }
    }
}
