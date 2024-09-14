using Azure.Core;
using Erox.Application.Base;
using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Erox.Application.Cards.Queries
{
    public class GetCard : QueryBase<CardItem>
    {
        public Guid UserId { get; set; }
        public class GetCardHandler : QueryBaseHandler<GetCard>
        {
            public GetCardHandler(DataContext ctx) : base(ctx)
            {
            }

            protected override IQueryable<CardItem> StructureItems()
            {
                return
                base.StructureItems()

                .Include(wi => wi.Product)
                .ThenInclude(wi => wi.ProductNameTranslations)

                .Include(wi => wi.Product)
                .ThenInclude(w => w.Images)

                .Include(wi => wi.Product)
                .ThenInclude(wi => wi.Sizes);
            }

            protected override IQueryable<CardItem> FilterItems(IQueryable<CardItem> query, GetCard request)
            {
                // Добавляем фильтр по ProductId
                return query.Where(review => review.Card.UserId == request.UserId);
            }
        }
    }
}
