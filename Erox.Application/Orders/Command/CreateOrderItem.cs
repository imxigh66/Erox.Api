using Erox.Application.Models;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.Command
{
    public class CreateOrderItem:IRequest<OperationResult<OrderItem>>
    {
       
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
