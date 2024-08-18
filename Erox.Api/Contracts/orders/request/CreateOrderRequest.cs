using Erox.Application.Orders.Command;
using Erox.Domain.Enumerations;

namespace Erox.Api.Contracts.orders.request
{
    public class CreateOrderRequest
    {
        public CreateOrderItem[] Items { get; set; }

        public StatusEnum Status { get; set; }
        public decimal Sum { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public ShippingMethodEnum ShippingMethod { get; set; }
        public string Address { get; set; }

    }
}
