using Erox.Application.Models;
using Erox.DataAccess;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Base
{
	public class QueryBase<TEntity> : IRequest<OperationResult<TEntity[]>>
	where TEntity : class
	{
		public abstract class QueryBaseHandler<TQuery> : IRequestHandler<TQuery, OperationResult<TEntity[]>>
		where TQuery : QueryBase<TEntity>
		{
			public readonly DataContext _dbContext;
			public QueryBaseHandler(DataContext dbContext)
			{
				_dbContext = dbContext;
			}

			public virtual async Task<OperationResult<TEntity[]>> Handle(TQuery request, CancellationToken cancellationToken)
			{
				var response = new OperationResult<TEntity[]>();
				try
				{
					var structuredQuery = StructureItems();
					var filterAndStructureQuery = FilterItems(structuredQuery, request);
					response.PayLoad = await filterAndStructureQuery.ToArrayAsync(cancellationToken);
				}
				catch (Exception e)
				{
					response.AddUnknownError(e.Message);
				}
				return response;
			}
			protected virtual IQueryable<TEntity> StructureItems()
			{
				return _dbContext.Set<TEntity>().AsNoTracking().AsSplitQuery();
			}
			protected virtual IQueryable<TEntity> FilterItems(IQueryable<TEntity> query, TQuery request) 
			{
				return query;
			}
		}
	}
}
