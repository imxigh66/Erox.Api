using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Erox.Application.Products.Queries
{
    public class GetAllCategories : QueryBase<Category>
    {
        public class GetAllCategoriesHandler : QueryBaseHandler<GetAllCategories>
        {

            public GetAllCategoriesHandler(DataContext ctx) : base(ctx)
            {
            }
            protected override IQueryable<Category> StructureItems()
            {
                return
                base.StructureItems()
                    .Include(i => i.CategoryTranslations);
            }
        }
    }
}
