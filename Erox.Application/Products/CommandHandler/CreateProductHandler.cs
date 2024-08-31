using Erox.Application.Models;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Exeptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
    public class CreateProductHandler : IRequestHandler<CreateProduct, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        public CreateProductHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<Product>> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();
            try
            {
                var product = Product.CreateProduct(request.Name,request.Description,request.Price,request.DiscountPrice, request.CategoryId, request.Color,request.Image,request.Season, request.Code);
                _ctx.Products.Add(product);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = product;
            }
            catch (ProductNotValidExeption e)
            {


                e.ValidationErrors.ForEach(er =>
                {
                    result.AddError(Enums.ErrorCode.ValidationError, er);
                });
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                result.AddUnknownError(innerExceptionMessage);
            }
            return result;
        }
    }
}
