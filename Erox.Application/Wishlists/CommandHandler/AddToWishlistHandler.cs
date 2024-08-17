using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Wishlists.Command;
using Erox.DataAccess;
using Erox.DataAccess.Migrations;
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
    public class AddToWishlistHandler : IRequestHandler<AddToWishlist, OperationResult<Wishlist>>
    {
        private readonly DataContext _ctx;
        public AddToWishlistHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        //public async Task<OperationResult<Wishlist>> Handle(AddToWishlist request, CancellationToken cancellationToken)
        //{
        //    var result = new OperationResult<Wishlist>();

        //    var wishlist = await _ctx.Wishlists
        //        .Include(w => w.Items)
        //        .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);



        //    // Проверяем, есть ли продукт в вишлисте (если нужно избегать дубликатов)
        //    var existingItem = wishlist.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

        //    if (existingItem != null)
        //    {
        //        result.AddError(ErrorCode.ValidationError, "Product already in wishlist.");
        //        return result;
        //    }

        //    var wishlistItem = new WishlistItem
        //    {
        //        WishlistId = wishlist.WishlistId,
        //        ProductId = request.ProductId
        //    };

        //    wishlist.Items.Add(wishlistItem);

        //    _ctx.Wishlists.Update(wishlist);
        //    await _ctx.SaveChangesAsync(cancellationToken);

        //    return result;
        //}

        public async Task<OperationResult<Wishlist>> Handle(AddToWishlist request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Wishlist>();

            try
            {
                // Пытаемся найти существующий вишлист пользователя
                var wishlist = await _ctx.Wishlists
                    .Include(w => w.Items)
                    .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

                // Если вишлист не найден, создаем новый
                if (wishlist == null)
                {
                    wishlist = new Wishlist
                    {
                        UserId = request.UserId,
                        Items = new List<WishlistItem>()  // Инициализируем коллекцию элементов
                    };
                    _ctx.Wishlists.Add(wishlist);
                    await _ctx.SaveChangesAsync(cancellationToken);  // Сохраняем новый вишлист в базу данных
                }

                // Проверяем, есть ли продукт в вишлисте (если нужно избегать дубликатов)
                var existingItem = wishlist.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

                if (existingItem != null)
                {
                    result.AddError(ErrorCode.ValidationError, "Product already in wishlist.");
                    return result;
                }

                // Добавляем новый элемент в вишлист
                var wishlistItem = new WishlistItem
                {
                    WishlistId = wishlist.WishlistId,
                    ProductId = request.ProductId
                };

                wishlist.Items.Add(wishlistItem);

                // Обновляем вишлист в базе данных
                _ctx.Wishlists.Update(wishlist);
                await _ctx.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                // Логируем внутреннюю ошибку для получения более детальной информации
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                result.AddUnknownError(innerExceptionMessage);
                return result;
            }
            catch (Exception ex)
            {
                // Общая обработка других ошибок
                result.AddUnknownError(ex.Message);
                return result;
            }

            return result;
        }


    }
}
