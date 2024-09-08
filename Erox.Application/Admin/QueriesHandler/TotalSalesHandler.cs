using Erox.Application.Admin.Query;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.QueriesHandler
{
    public class TotalSalesHandler : IRequestHandler<TotalSalesQuery, TotalSalesDto>
    {
        public readonly DataContext _ctx;
        public TotalSalesHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<TotalSalesDto> Handle(TotalSalesQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var query = _ctx.Set<Order>()
        .Where(o => o.Status == StatusEnum.Cancelled.ToString())
        // Показываем только заказы со статусом "Cancelled"
        .AsQueryable();

            // Общее количество заказов за сегодня
            var todayCount = request.GetTodayCount
                ? await query.Where(o => o.CreatedDate >= today && o.CreatedDate < today.AddDays(1)).CountAsync(cancellationToken)
                : 0;

            // Общее количество заказов за неделю
            var thisWeekCount = request.GetThisWeekCount
                ? await query.Where(o => o.CreatedDate >= startOfWeek && o.CreatedDate < startOfWeek.AddDays(7)).CountAsync(cancellationToken)
                : 0;

            // Общее количество заказов за месяц
            var thisMonthCount = request.GetThisMonthCount
                ? await query.Where(o => o.CreatedDate >= startOfMonth && o.CreatedDate < startOfMonth.AddMonths(1)).CountAsync(cancellationToken)
                : 0;

            // Общее количество всех заказов
            var totalCount = request.GetTotalCount
                ? await query.CountAsync(cancellationToken)
                : 0;

            return new TotalSalesDto
            {
                TodayCount = todayCount,
                ThisWeekCount = thisWeekCount,
                ThisMonthCount = thisMonthCount,
                TotalCount = totalCount
            };
        }
    }
}
