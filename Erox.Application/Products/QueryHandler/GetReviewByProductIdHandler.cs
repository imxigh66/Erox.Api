using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
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
    public class GetReviewByProductIdHandler : IRequestHandler<GetReviewByProductId, OperationResult<List<ProductReview>>>
    {
        private readonly DataContext _ctx;
        public GetReviewByProductIdHandler(DataContext ctx)
        {
                _ctx=ctx;
        }
        public async Task<OperationResult<List<ProductReview>>> Handle(GetReviewByProductId request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<ProductReview>>();
            try
            {
                var product = await _ctx.Products
                    .Include(p => p.Reviews)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

                result.PayLoad = product.Reviews.ToList();
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
