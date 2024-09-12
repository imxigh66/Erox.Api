using Erox.Application.Base;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Products.Queries
{
	public class GetProductByFilter : QueryBase<Product>
	{
		public Guid? ProductId	{ get; set; }
		public Guid? CategoryId {  get; set; }
		public string? CategoryName { get; set; }
		public string? Season	{ get; set; }
		public string? Code		{ get; set; }
		public decimal? Price	{ get; set; }

		public class GetProductbyFilterHandler : QueryBaseHandler<GetProductByFilter>
		{
			public GetProductbyFilterHandler(DataContext ctx) : base(ctx)
			{
			}

			protected override IQueryable<Product> StructureItems()
			{
				return 
				base.StructureItems()
					.Include(i => i.Sizes)
					.Include(i => i.Category).ThenInclude(i => i.CategoryTranslations)
					.Include(i => i.ProductNameTranslations)
					.Include(i => i.ProductDescriptionTranslations)
					.Include(i => i.Images);
			}

			protected override IQueryable<Product> FilterItems(IQueryable<Product> query, GetProductByFilter request)
			{
				query =  base.FilterItems(query, request);
				// Фильтр по UserId
				if (request.CategoryId != null)
				{
					query = query.Where(o => o.CategoryId == request.CategoryId);
				}

				if(request.CategoryName != null) 
				{
					query = query.Where(w => w.Category.CategoryTranslations.Any(a => a.Title == request.CategoryName));
				}

				if (request.ProductId != null)
				{
					query = query.Where(o => o.ProductId == request.ProductId);
				}

				// Фильтр по дате создания
				if (request.Season != null)
				{
					query = query.Where(o => o.Season == request.Season);
				}
				if (request.Code != null)
				{
					query = query.Where(o => o.Code == request.Code);
				}

				if (request.Price != null)
				{
					query = query.Where(o => o.Price == request.Price);
				}
				
				return query;
			}
		}
	}
}
