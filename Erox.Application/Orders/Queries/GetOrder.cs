using Erox.Application.Base;
using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Erox.Application.Orders.Queries
{
    public class GetOrder : QueryBase<Order>
    {
        public Guid? UserId { get; set; }
        public Guid? OrderId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }

        public class GetOrderHandler : QueryBaseHandler<GetOrder>
        {

            public GetOrderHandler(DataContext ctx) : base(ctx)
            {
            }
            protected override IQueryable<Order> StructureItems()
            {
                return
                base.StructureItems()
                    .Include(i => i.Items).ThenInclude(i=>i.Product).ThenInclude(p=>p.ProductNameTranslations)
                    .Include(i => i.Items).ThenInclude(i => i.Product).ThenInclude(p => p.Images)
                    .Include(i=>i.Items).ThenInclude(i=>i.Size);
            }
            protected override IQueryable<Order> FilterItems(IQueryable<Order> query, GetOrder request)
            {
                query = base.FilterItems(query, request);
                if (request.UserId != null)
                {
                    query = query.Where(o => o.UserId == request.UserId);
                }

                // Фильтр по OrderId
                if (request.OrderId != null)
                {
                    query = query.Where(o => o.OrderId == request.OrderId);
                }

                // Фильтр по статусу
                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(o => o.Status == request.Status);
                }

                // Фильтр по дате создания
                if (request.CreatedDate != null)
                {
                    query = query.Where(o => o.CreatedDate.Date == request.CreatedDate.Value.Date);
                }
                return query;
            }
        }
    }
}
