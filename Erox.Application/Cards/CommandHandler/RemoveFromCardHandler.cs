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
    public class RemoveFromCardHandler : IRequestHandler<RemoveFromCard, OperationResult<CardItem>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<CardItem> _result;
        public RemoveFromCardHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<CardItem>();
        }
        public async Task<OperationResult<CardItem>> Handle(RemoveFromCard request, CancellationToken cancellationToken)
        {
            var card = await _ctx.Cards
            .Include(w => w.Items)
            .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

            if (card == null)
            {
                _result.AddError(ErrorCode.NotFound, "Card not found.");
                return _result;
            }

            // Ищем элемент вишлиста по ProductId
            var cardItem = card.Items.FirstOrDefault(i => i.ProductId == request.ProductId);

            if (cardItem == null)
            {
                _result.AddError(ErrorCode.NotFound, "Card item not found.");
                return _result;
            }

            // Удаляем элемент из вишлиста
            card.Items.Remove(cardItem);
            _ctx.Cards.Update(card);
            await _ctx.SaveChangesAsync(cancellationToken);

            // Возвращаем удаленный элемент
            _result.PayLoad = cardItem;
            return _result;
        }
    }
}
