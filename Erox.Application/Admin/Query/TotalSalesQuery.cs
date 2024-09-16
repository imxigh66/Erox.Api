using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Admin.Query
{
	public class TotalSalesQuery : QueryBase<TotalSalesDto>
	{
		public bool GetTodayCount		{ get; set; }
		public bool GetThisWeekCount	{ get; set; }
		public bool GetThisMonthCount	{ get; set; }
		public bool GetTotalCount		{ get; set; }

		public class TotalSalesHandler : QueryBaseHandler<TotalSalesQuery>
		{
			public TotalSalesHandler(DataContext ctx) : base(ctx) { }

			public async override Task<OperationResult<TotalSalesDto[]>> Handle(TotalSalesQuery request, CancellationToken cancellationToken)
			{
				var today = DateTime.Today;
				var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
				var startOfMonth = new DateTime(today.Year, today.Month, 1);

				var query = _dbContext	.Set<Order>().AsNoTracking().AsSplitQuery()
										.Where(o => o.Status == StatusEnum.Delivered.ToString())
										// Показываем только заказы со статусом "Delivered"
										.AsQueryable();

				// Общее количество заказов за сегодня
				var todayCount		= request.GetTodayCount
									? await query.Where(o => o.CreatedDate >= today			&& o.CreatedDate < today.AddDays(1)).CountAsync(cancellationToken)
									: 0;

				// Общее количество заказов за неделю
				var thisWeekCount	= request.GetThisWeekCount
									? await query.Where(o => o.CreatedDate >= startOfWeek	&& o.CreatedDate < startOfWeek.AddDays(7)).CountAsync(cancellationToken)
									: 0;

				// Общее количество заказов за месяц
				var thisMonthCount	= request.GetThisMonthCount
									? await query.Where(o => o.CreatedDate >= startOfMonth	&& o.CreatedDate < startOfMonth.AddMonths(1)).CountAsync(cancellationToken)
									: 0;

				// Общее количество всех заказов
				var totalCount		= request.GetTotalCount
									? await query.CountAsync(cancellationToken)
									: 0;

				return new OperationResult<TotalSalesDto[]> {
					PayLoad = [ new TotalSalesDto {
						TodayCount		= todayCount,
						ThisWeekCount	= thisWeekCount,
						ThisMonthCount	= thisMonthCount,
						TotalCount		= totalCount
					} ]
				};
			}
		}
	}
}
