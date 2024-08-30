using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.CardAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.QueryHandler
{
    public class GetSizesByProductIdHandler : IRequestHandler<GetSizesByProductId, OperationResult<ProductSize[]>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<ProductSize[]> _result;
        public GetSizesByProductIdHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<ProductSize[]>();
        }
        public async  Task<OperationResult<ProductSize[]>> Handle(GetSizesByProductId request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _ctx.Products
                    .Include(p => p.Sizes)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

                _result.PayLoad = product.Sizes.ToArray();
            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }
            return _result;
        }
    }
}
