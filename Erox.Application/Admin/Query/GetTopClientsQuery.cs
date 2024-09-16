using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Admin.Query
{
	public class GetTopClientsQuery : QueryBase<TopClientDto>
	{
		public int TopCount { get; set; }

		public GetTopClientsQuery(int topCount)
		{
			TopCount = topCount;
		}

		public class GetTopClientsQueryHandler : QueryBaseHandler<GetTopClientsQuery>
		{
			public GetTopClientsQueryHandler(DataContext ctx) : base(ctx) { }

			public override async Task<OperationResult<TopClientDto[]>> Handle(GetTopClientsQuery request, CancellationToken cancellationToken)
			{
				var response = new OperationResult<TopClientDto[]>();
				try
				{
					response.PayLoad = await _dbContext
					.Set<Order>().AsNoTracking().AsSplitQuery()
					.Where	(w => w.Status == StatusEnum.Delivered.ToString())
					.GroupBy(g => g.UserId)
					.Select	(s => new TopClientDto
					{
						UserId = s.Key,                     // Id пользователя
						TotalOrders = s.Count(),            // Количество заказов
						TotalSum = s.Sum(o => o.Sum)        // Общая сумма покупок
					})
					.OrderByDescending(x => x.TotalOrders)  // Можно отсортировать по TotalOrders или TotalSum
					.Take(request.TopCount)                 // Берем топ N клиентов
					.ToArrayAsync(cancellationToken);
				}
				catch (Exception e)
				{
					response.AddUnknownError(e.Message);
				}
				return response;
			}
		}
	}
}
