using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Orders.Queries;
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

namespace Erox.Application.Orders.Queryhandler
{
    public class GetOrderHandler : IRequestHandler<GetOrder, OperationResult<Order[]>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order[]> _result;
        public GetOrderHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<Order[]>();
        }
        public async Task<OperationResult<Order[]>> Handle(GetOrder request, CancellationToken cancellationToken)
        {
            try
            {
                // Фильтрация заказа по переданным параметрам
                var query = _ctx.Orders.AsQueryable();

                // Фильтр по UserId
                if (request.UserId != null)
                {
                    query = query.Where(o => o.UserId == request.UserId);
                }

                // Фильтр по OrderId
                if (request.OrderId != null)
                {
                    query = query.Where(o => o.OrderId == request.OrderId);
                }

                // Фильтр по статусу
                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(o => o.Status == request.Status);
                }

                // Фильтр по дате создания
                if (request.CreatedDate != null)
                {
                    query = query.Where(o => o.CreatedDate.Date == request.CreatedDate.Value.Date);
                }

                query = query.Include(i => i.Items).ThenInclude(t=>t.Product);
                query = query.Include(i => i.Items).ThenInclude(t=>t.Size);

                // Получение заказов
                var orders = await query.ToArrayAsync(cancellationToken);
                if (orders == null)
                {
                    _result.AddError(ErrorCode.NotFound, "Order not found.");
                    return _result;
                }



                // Возвращаем элементы вишлиста
                _result.PayLoad = orders;
                return _result;
            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
                return _result;
            }
        
    }
    }
}
