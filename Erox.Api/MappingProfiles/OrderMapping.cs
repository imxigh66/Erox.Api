using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.orders.response;
using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
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
            CreateMap<OrderItem, OrderItemResponse>();
            
        }
    }
}
