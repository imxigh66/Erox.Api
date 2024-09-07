using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Orders.Command;
using Erox.Application.Products;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.CommandHandler
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrder, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order> _result;
        public UpdateOrderHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<Order>();
        }
        public async Task<OperationResult<Order>> Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            

            try
            {
                var order = await _ctx.Orders.FirstOrDefaultAsync(p => p.OrderId == request.OrderId, cancellationToken: cancellationToken);

                if (order is null)
                {
                    _result.AddError(ErrorCode.NotFound, string.Format(OrderErrorMessage.OrderNotFound, request.OrderId));
                    return _result;
                }
                order.UpdateOrder(request.Status, request.PaymentMethod, request.ShippingMethod, request.Sum, request.Address,
                    request.Items.Select(i=>new OrderItem {
                        SizeId=i.SizeId,Quantity=i.Quantity,ProductId=i.ProductId,OrderItemId=i.OrderItemId}).ToArray());
                
                await _ctx.SaveChangesAsync(cancellationToken);
                _result.PayLoad = order;
                return _result;
            }
            catch (ProductNotValidExeption e)
            {

                e.ValidationErrors.ForEach(er =>
                {
                    _result.AddError(ErrorCode.ValidationError, er);
                });
            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            return _result;
        }
    }
}
