using Erox.Application.Enums;
using Erox.Application.Models;

using Erox.Application.Wishlists.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Wishlists.CommandHandler
{
    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlist, OperationResult<WishlistItem>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<WishlistItem> _result;
        public RemoveFromWishlistHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result = new OperationResult<WishlistItem>();
        }
        public async Task<OperationResult<WishlistItem>> Handle(RemoveFromWishlist request, CancellationToken cancellationToken)
        {
            var wishlist = await _ctx.Wishlists
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

            if (wishlist == null)
            {
                _result.AddError(ErrorCode.NotFound, "Wishlist not found.");
                return _result;
            }

            // Ищем элемент вишлиста по ProductId
            var wishlistItem = wishlist.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (wishlistItem == null)
            {
                _result.AddError(ErrorCode.NotFound, "Wishlist item not found.");
                return _result;
            }

            // Удаляем элемент из вишлиста
            wishlist.Items.Remove(wishlistItem);
            _ctx.Wishlists.Update(wishlist);
            await _ctx.SaveChangesAsync(cancellationToken);

            // Возвращаем удаленный элемент
            _result.PayLoad = wishlistItem;
            return _result;
        }
    }
}
