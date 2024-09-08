using Erox.Application.Models;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
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
    public class GetAllReviewsHandler : IRequestHandler<GetAllReviews, OperationResult<List<ProductReview>>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<List<ProductReview>> _result;
        public GetAllReviewsHandler(DataContext ctx)
        {
            _ctx = ctx;
            _result = new OperationResult<List<ProductReview>>();
        }
        public async Task<OperationResult<List<ProductReview>>> Handle(GetAllReviews request, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _ctx.ProductReviews
           .Include(i => i.Product)
               .ThenInclude(p => p.ProductNameTranslations)
               
           .ToListAsync();

                _result.PayLoad = review;
            }
            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);

            }
            return _result;
        }
    }
}
