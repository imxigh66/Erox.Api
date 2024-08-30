using Erox.Application.Cards.Queries;
using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Cards.QueryHandler
{
    public class GetCardHandler : IRequestHandler<GetCard, OperationResult<List<CardItem>>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<List<CardItem>> _result;
        public GetCardHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<List<CardItem>>();
        }
        public async Task<OperationResult<List<CardItem>>> Handle(GetCard request, CancellationToken cancellationToken)
        {
            var card = await _ctx.Cards
                .Include(w => w.Items)
                .ThenInclude(wi => wi.Product)  // Загружаем связанные продукты
                .FirstOrDefaultAsync(w => w.UserId == request.UserId, cancellationToken);

            if (card == null)
            {
                _result.AddError(ErrorCode.NotFound, "Cart not found.");
                return _result;
            }

            // Возвращаем элементы вишлиста
            _result.PayLoad = card.Items.ToList();
            return _result;
        }
    }
}
