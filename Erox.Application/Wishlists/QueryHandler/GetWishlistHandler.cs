using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Wishlists.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Wishlists.QueryHandler
{
    public class GetWishlistHandler : IRequestHandler<GetWishlist, OperationResult<List<WishlistItem>>>
    {
        private readonly DataContext _ctx;
        public GetWishlistHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<List<WishlistItem>>> Handle(GetWishlist request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<WishlistItem>>();

            // Находим вишлист пользователя вместе с продуктами
            var wishlist = await _ctx.Wishlists
                .Include(w => w.Items)
                .ThenInclude(wi => wi.Product).ThenInclude(i=>i.ProductNameTranslations)
                .Include(w => w.Items)
                .ThenInclude(wi => wi.Product).ThenInclude(i => i.Images)// Загружаем связанные продукты
                .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

            if (wishlist == null)
            {
                result.AddError(ErrorCode.NotFound, "Wishlist not found.");
                return result;
            }

            // Возвращаем элементы вишлиста
            result.PayLoad = wishlist.Items.ToList();
            return result;
        }
    }
}
