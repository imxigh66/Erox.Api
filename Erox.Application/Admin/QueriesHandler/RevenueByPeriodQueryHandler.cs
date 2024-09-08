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
    public class RevenueByPeriodQueryHandler : IRequestHandler<RevenueByPeriodQuery, decimal>
    {
        private readonly DataContext _ctx;

        public RevenueByPeriodQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<decimal> Handle(RevenueByPeriodQuery request, CancellationToken cancellationToken)
        {

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.UtcNow;

            // Определяем диапазон дат в зависимости от типа периода
            switch (request.PeriodType)
            {
                case PeriodType.Day:
                    startDate = DateTime.UtcNow.Date; // Сегодня
                    endDate = startDate.AddDays(1); // Завтра
                    break;

                case PeriodType.Week:
                    startDate = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek); // Начало недели
                    endDate = startDate.AddDays(7); // Конец недели
                    break;

                case PeriodType.Month:
                    startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1); // Начало месяца
                    endDate = startDate.AddMonths(1); // Конец месяца
                    break;

                case PeriodType.AllTime:
                    startDate = DateTime.MinValue; // Всё время
                    endDate = DateTime.UtcNow;
                    break;
            }

            // Считаем доход за выбранный период
            var totalRevenue = await _ctx.Orders
     .Where(o => o.Status == StatusEnum.Cancelled.ToString() && o.CreatedDate.Date >= startDate.Date && o.CreatedDate.Date <= endDate.Date)
     .SumAsync(o => o.Sum);


            return totalRevenue;
        
    }
    }
}
