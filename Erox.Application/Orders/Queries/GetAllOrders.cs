using Erox.Application.Base;
using Erox.Application.Models;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Orders.Queries
{
    public class GetAllOrders : QueryBase<Order>
    {
        public class GetAllOrdersHandler : QueryBaseHandler<GetAllOrders>
        {
            public GetAllOrdersHandler(DataContext ctx) : base(ctx)
            {
            }
            protected override IQueryable<Order> StructureItems()
            {
                return
                base.StructureItems()
                    .Include(i => i.Items);
            }
        }
    }
}
