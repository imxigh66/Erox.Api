using Erox.Application.Admin.Query;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.QueriesHandler
{
    public class GetTopClientsQueryHandler : IRequestHandler<GetTopClientsQuery, List<TopClientDto>>
    {
        private readonly DataContext _ctx;
        public GetTopClientsQueryHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<List<TopClientDto>> Handle(GetTopClientsQuery request, CancellationToken cancellationToken)
        {
            // Группируем заказы по пользователям и подсчитываем как количество заказов, так и общую сумму
            var topClients = await _ctx.Set<Order>()
                .Where(o => o.Status == "Created")
                .GroupBy(o => o.UserId)
                .Select(group => new TopClientDto
                {
                    UserId = group.Key,                 // Id пользователя
                    TotalOrders = group.Count(),        // Количество заказов
                    TotalSum = group.Sum(o => o.Sum)    // Общая сумма покупок
                })
                .OrderByDescending(x => x.TotalOrders)  // Можно отсортировать по TotalOrders или TotalSum
                .Take(request.TopCount)                 // Берем топ N клиентов
                .ToListAsync(cancellationToken);

            return topClients;
        }
    }
}
