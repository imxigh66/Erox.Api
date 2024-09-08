using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.orders.response;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Enumerations;

namespace Erox.Api.MappingProfiles
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderResponse>();
            CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<StatusEnum>(src.Status)))
            .ForMember(dest => dest.PaymenentMethod, opt => opt.MapFrom(src => Enum.Parse<PaymentMethodEnum>(src.PaymenentMethod)))
            .ForMember(dest => dest.ShippingMethod, opt => opt.MapFrom(src => Enum.Parse<ShippingMethodEnum>(src.ShippingMethod)))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ReverseMap();
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Product.ProductNameTranslations))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Product.Code))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src => src.Product.DiscountPrice))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Product.Images.Select(img => $"{img.Path}").ToList()));
            CreateMap<UserProfileEntity, UserInfoResponse>()
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Basicinfo.Firstname))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Basicinfo.Lastname))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Basicinfo.EmailAddress))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Basicinfo.Phone));
        }
    }
}
