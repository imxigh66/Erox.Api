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
    public class DeleteOrder:IRequest<OperationResult<Order>>
    {
        public Guid OrderId { get; set; }
    }
}
