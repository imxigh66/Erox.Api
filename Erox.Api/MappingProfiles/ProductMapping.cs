using AutoMapper;
using Erox.Api.Contracts.posts.responses;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.Translations;

namespace Erox.Api.MappingProfiles
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductResponce>()
                .ForMember(des=>des.Names,opt=>opt.MapFrom(src=>src.ProductNameTranslations))
                .ForMember(des=>des.Descriptions,opt=>opt.MapFrom(src=>src.ProductDescriptionTranslations))
                 .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => img.Path).ToArray()));

            CreateMap<ProductNameTranslation, ProductTranslationResponse>()
                .ForMember(d=>d.LanguageCode,o=>o.MapFrom(s=>s.Language));

            CreateMap<ProductDescriptionTranslation, ProductTranslationResponse>()
                .ForMember(d => d.LanguageCode, o => o.MapFrom(s => s.Language));

            CreateMap<ProductReview,ProductReviewResponse>();
            CreateMap<ProductSize, ProductSizeResponse>();
       
            CreateMap<ProductSize[], GetSizesByProductsIdResponse>()
    .ForMember(dest => dest.Sizes, opt => opt.MapFrom(src => src));
        }
    }
}
