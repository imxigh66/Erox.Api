using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Erox.Application.Products.Queries
{
    public class GetSizesByProductId : QueryBase<ProductSize>
    {
        public Guid ProductId { get; set; }
        public class GetSizesByProductIdHandler : QueryBaseHandler<GetSizesByProductId>
        {
            public GetSizesByProductIdHandler(DataContext ctx) : base(ctx)
            {
            }

            protected override IQueryable<ProductSize> StructureItems()
            {
                return
                base.StructureItems()
                    .Include(p => p.Product);
            }
            protected override IQueryable<ProductSize> FilterItems(IQueryable<ProductSize> query, GetSizesByProductId request)
            {
                // Добавляем фильтр по ProductId
                return query.Where(size => size.ProductId == request.ProductId);
            }
        }

    }
}
