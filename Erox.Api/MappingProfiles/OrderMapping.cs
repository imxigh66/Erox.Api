using AutoMapper;
using Erox.Api.Contracts.cards.response;
using Erox.Api.Contracts.orders.response;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;

namespace Erox.Api.MappingProfiles
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<Order, CreateOrderResponse>();
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
