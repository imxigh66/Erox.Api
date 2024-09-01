using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
    public class AddProductReviewHandler : IRequestHandler<AddProductReview, OperationResult<ProductReview>>
    {
        private readonly DataContext _ctx;
        public AddProductReviewHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<ProductReview>> Handle(AddProductReview request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ProductReview>();

            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

                if (product is null)
                {

                    result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound, request.ProductId));
                    return result;
                }

                var review = ProductReview.CreateProductReview(request.ProductId, request.Text,request.IsApproved);
              

                product.AddProductReview(review);

                _ctx.Products.Update(product);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = review;

            }
            catch (ProductReviewNotValidException e)
            {


                e.ValidationErrors.ForEach(er =>
                {

                    result.AddError(ErrorCode.ValidationError, er);
                });
            }

            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }
            return result;
        }
    }
}
