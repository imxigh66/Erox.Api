using Erox.Application.Models;

using Erox.Application.Products.Command;
using Erox.DataAccess;

using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
    public class DeleteReviewHandler : IRequestHandler<DeleteReview, OperationResult<ProductReview>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<ProductReview> _result;
        public DeleteReviewHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result = new OperationResult<ProductReview>();
        }
        public async Task<OperationResult<ProductReview>> Handle(DeleteReview request, CancellationToken cancellationToken)
        {
            var product = await _ctx.Products.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);
            if (product == null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, ProductsErrorMessage.ProductNotFound);
                return _result;
            }

            var review =product.Reviews.FirstOrDefault(c => c.ReviewId == request.ReviewId);
            if (review == null)
            {
                _result.AddError(Enums.ErrorCode.NotFound, ProductsErrorMessage.ReviewNotFound);
                return _result;
            }
            //if (review.UserProfieId != request.UserProfileId)
            //{
            //    _result.AddError(Enums.ErrorCode.RemoveReviewNotAuthorized, ProductsErrorMessage.RemoveReviewNotAuthorized);
            //    return _result;
            //}

            product.ARemoveReview(review);
            _ctx.Products.Update(product);
            await _ctx.SaveChangesAsync(cancellationToken);
            _result.PayLoad = review;
            return _result;
        }
    }
}
