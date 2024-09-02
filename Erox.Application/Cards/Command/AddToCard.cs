using Erox.Application.Models;
using Erox.DataAccess.Migrations;
using Erox.Domain.Aggregates.CardAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Cards.Command
{
    public class AddToCard:IRequest<OperationResult<Card>>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductSizeId { get; set; }
       
        public int Quantity { get; set; }    

    }
}
