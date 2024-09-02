using Erox.Application.Cards.Command;
using Erox.Application.Enums;
using Erox.Application.Models;
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

namespace Erox.Application.Cards.CommandHandler
{
    public class AddToCardHandler : IRequestHandler<AddToCard, OperationResult<Card>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Card> _result;
        public AddToCardHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result = new OperationResult<Card>();
        }
        public async Task<OperationResult<Card>> Handle(AddToCard request, CancellationToken cancellationToken)
        {
            try
            {
                // Пытаемся найти существующий вишлист пользователя
                var card = await _ctx.Cards
                    .Include(w => w.Items)
                    .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

                // Если вишлист не найден, создаем новый
                if (card == null)
                {
                    card = new Card
                    {
                        UserId = request.UserId,
                        
                        Items = new List<CardItem>()  // Инициализируем коллекцию элементов
                    };
                    _ctx.Cards.Add(card);
                    await _ctx.SaveChangesAsync(cancellationToken);  // Сохраняем новый вишлист в базу данных
                }

                // Проверяем, есть ли продукт в вишлисте (если нужно избегать дубликатов)
                var existingItem = card.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

                if (existingItem != null)
                {
                    _result.AddError(ErrorCode.ValidationError, "Product already in card.");
                    return _result;
                }

                // Добавляем новый элемент в вишлист
                var cardItem = new CardItem
                {
                    CardId = card.CardId,
                   SizeId=request.ProductSizeId,
                 
                   Quantity = request.Quantity,
                    ProductId = request.ProductId
                };

                card.Items.Add(cardItem);

                // Обновляем вишлист в базе данных
                _ctx.Cards.Update(card);
                await _ctx.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                // Логируем внутреннюю ошибку для получения более детальной информации
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                _result.AddUnknownError(innerExceptionMessage);
                return _result;
            }
            catch (Exception ex)
            {
                // Общая обработка других ошибок
                _result.AddUnknownError(ex.Message);
                return _result;
            }

            return _result;
        }
    }
}
