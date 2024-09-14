using Erox.Application.Base;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;

using Microsoft.EntityFrameworkCore;

namespace Erox.Application.Products.Queries
{
	public class GetAllProducts: QueryBase<Product>    
    {
		public class GetAllProductsHandler : QueryBaseHandler<GetAllProducts>
		{
			public GetAllProductsHandler(DataContext ctx) : base(ctx) 
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
		}
	}
}
