using Erox.Application.Models;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.Command
{
    public class CreateOrder:IRequest<OperationResult<Order>>
    {
        public Guid UserId { get; set; }
        public CreateOrderItem[] Items { get; set; }
     
        public string Status { get; set; }
        public decimal Sum { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingMethod { get; set; }
        public string Address { get; set; }
        
    }
}
