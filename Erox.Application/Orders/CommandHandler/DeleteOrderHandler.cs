using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Orders.Command;
using Erox.Application.Products;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.CommandHandler
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrder, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order> _result;
        public DeleteOrderHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<Order>();
        }
        public async Task<OperationResult<Order>> Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _ctx.Orders.FirstOrDefaultAsync(p => p.OrderId == request.OrderId);
                if (order is null)
                {

                    _result.AddError(ErrorCode.NotFound, string.Format(OrderErrorMessage.OrderNotFound, request.OrderId));
                    return _result;
                }


                _ctx.Orders.Remove(order);
                await _ctx.SaveChangesAsync(cancellationToken);
                _result.PayLoad = order;
            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }
            return _result;
        }
    }
}
