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
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image));



        }
    }
}
