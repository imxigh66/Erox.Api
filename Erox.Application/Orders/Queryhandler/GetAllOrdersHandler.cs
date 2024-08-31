using Erox.Application.Models;
using Erox.Application.Orders.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.Queryhandler
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrders, OperationResult<List<Order>>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<List<Order>> _result;
        public GetAllOrdersHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<List<Order>>();
        }
        public async Task<OperationResult<List<Order>>> Handle(GetAllOrders request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _ctx.Orders.Include(o => o.Items).ToListAsync();
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
