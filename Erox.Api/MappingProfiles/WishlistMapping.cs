using AutoMapper;
using Erox.Api.Contracts.wishlist.response;
using Erox.Domain.Aggregates.WishlistAggregate;

namespace Erox.Api.MappingProfiles
{
    public class WishlistMapping:Profile
    {
        public WishlistMapping()
        {
            CreateMap<Wishlist, AddWishlistResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Items.First().ProductId));



            CreateMap<WishlistItem, WishlistProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
            .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src => src.Product.DiscountPrice))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price)).
            ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Product.Images.Select(img => $"{img.Path}").ToList()))
            .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Product.ProductNameTranslations));



        }
    }
}
