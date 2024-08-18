using Erox.Application.Models;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.Queries
{
    public class GetOrder:IRequest<OperationResult<Order[]>>
    {
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
