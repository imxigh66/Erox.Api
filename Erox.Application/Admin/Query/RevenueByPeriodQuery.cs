using Erox.Application.Base;
using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Admin.Query
{
	public class RevenueByPeriodQuery : QueryBase<RevenueByPeriodDTO>
	{
		public PeriodType PeriodType { get; set; }

		public RevenueByPeriodQuery(PeriodType periodType)
		{
			PeriodType = periodType;
		}

		public class RevenueByPeriodQueryHandler : QueryBaseHandler<RevenueByPeriodQuery>
		{
			public RevenueByPeriodQueryHandler(DataContext ctx) : base(ctx) { }

			public async override Task<OperationResult<RevenueByPeriodDTO[]>> Handle(RevenueByPeriodQuery request, CancellationToken cancellationToken)
			{
				DateTime startDate = DateTime.MinValue;
				DateTime endDate = DateTime.UtcNow;

				// Определяем диапазон дат в зависимости от типа периода
				switch (request.PeriodType)
				{
					case PeriodType.Day:
						startDate = DateTime.UtcNow.Date;	// Сегодня
						endDate = startDate.AddDays(1);		// Завтра
						break;

					case PeriodType.Week:
						startDate = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek); // Начало недели
						endDate = startDate.AddDays(7);		// Конец недели
						break;

					case PeriodType.Month:
						startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1); // Начало месяца
						endDate = startDate.AddMonths(1);	// Конец месяца
						break;

					case PeriodType.AllTime:
						startDate = DateTime.MinValue;		// Всё время
						endDate = DateTime.UtcNow;
						break;
				}

				// Считаем доход за выбранный период
				var totalRevenue = await _dbContext.Orders.AsNoTracking().AsSplitQuery()
				.Where(o => o.Status == StatusEnum.Delivered.ToString() && o.CreatedDate.Date >= startDate.Date && o.CreatedDate.Date <= endDate.Date)
				.SumAsync(o => o.Sum, cancellationToken);

				return new OperationResult<RevenueByPeriodDTO[]> {
					PayLoad = [new RevenueByPeriodDTO { Revenue = totalRevenue }]
				};
			}
		}
	}
}
