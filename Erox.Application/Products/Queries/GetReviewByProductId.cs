using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;


namespace Erox.Application.Products.Queries
{
    public class GetReviewByProductId : QueryBase<ProductReview>
    {
        public Guid ProductId { get; set; }
        public class GetReviewByProductIdHandler : QueryBaseHandler<GetReviewByProductId>
        {
            public GetReviewByProductIdHandler(DataContext ctx) : base(ctx)
            {
            }
            protected override IQueryable<ProductReview> StructureItems()
            {
                return
                base.StructureItems()
                    .Include(p => p.Product);
            }
            protected override IQueryable<ProductReview> FilterItems(IQueryable<ProductReview> query, GetReviewByProductId request)
            {
                // Добавляем фильтр по ProductId
                return query.Where(review => review.Productid == request.ProductId);
            }
        }
    }
}
