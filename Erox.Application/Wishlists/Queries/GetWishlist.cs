using Erox.Application.Base;
using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Wishlists.Queries
{
    public class GetWishlist : QueryBase<WishlistItem>
    {
        public Guid UserId { get; set; }
        public class GetWishlistHandler : QueryBaseHandler<GetWishlist>
        {
            public GetWishlistHandler(DataContext ctx) : base(ctx)
            {
            }

            protected override IQueryable<WishlistItem> StructureItems()
            {
                return
                base.StructureItems()

                .Include(i => i.Product).ThenInclude(p => p.ProductNameTranslations)
                    .Include(i => i.Product).ThenInclude(p => p.Images);


            }
           
        }
    }
}
