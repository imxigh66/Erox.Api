using Erox.Application.Models;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Cards.Command
{
    public class RemoveFromCard: IRequest<OperationResult<CardItem>>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
