using Erox.Application.Models;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Cards.Queries
{
    public class GetCard: IRequest<OperationResult<List<CardItem>>>
    {
        public Guid UserId { get; set; }
    }
}
