using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Enumerations;

namespace Erox.Api.Contracts.orders.response
{
    public class OrderResponse
    {
        public Guid OrderId { get; set; }


        public Guid UserId { get; set; }
        
        public StatusEnum Status { get; set; }
        public PaymentMethodEnum PaymenentMethod { get; set; }
        public ShippingMethodEnum ShippingMethod { get; set; }
        public decimal Sum { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public OrderItemResponse[] Items { get; set; }
        

    }
}
