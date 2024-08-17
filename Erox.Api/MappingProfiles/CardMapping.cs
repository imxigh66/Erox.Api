using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.wishlist.response;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;

namespace Erox.Api.MappingProfiles
{
    public class CardMapping:Profile
    {
        public CardMapping()
        {
        //    CreateMap<Card, CreateCardResponse>()
        //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
        //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Items.First().ProductId));
            CreateMap<CardItem, CardProductResponse>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));
           
        }
    }
}
