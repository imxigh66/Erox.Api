using Erox.Application.Enums;
using Erox.Application.Models;
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
    public class CreateProductSizeHandler : IRequestHandler<CreateProductSize, OperationResult<ProductSize[]>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<ProductSize[]> _result;
        public CreateProductSizeHandler(DataContext ctx)
        {
                _ctx = ctx;
            _result = new OperationResult<ProductSize[]>();
        }
        public async Task<OperationResult<ProductSize[]>> Handle(CreateProductSize request, CancellationToken cancellationToken)
        {

            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

                if (product is null)
                {

                    _result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound, request.ProductId));
                    return _result;
                }

                var sizes=request.Sizes.Select(s=>new ProductSize() { ProductId=request.ProductId,Size=s});
                foreach (var size in sizes) { product.AddProductSize(size); }

                

                _ctx.Products.Update(product);
                await _ctx.SaveChangesAsync(cancellationToken);
                _result.PayLoad = sizes.ToArray();

            }
            catch (ProductReviewNotValidException e)
            {


                e.ValidationErrors.ForEach(er =>
                {

                    _result.AddError(ErrorCode.ValidationError, er);
                });
            }

            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);
            }
            return _result;
        }
    }
}
