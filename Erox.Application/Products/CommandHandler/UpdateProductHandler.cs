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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Erox.Application.Products.CommandHandler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        public UpdateProductHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<Product>> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();

            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken: cancellationToken);

                if (product is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound, request.ProductId));
                    return result;
                }
                product.UpdateProducts(request.Name, request.Description, request.Price, request.DiscountPrice, request.Category, request.Size, request.Color, request.Image,request.Season, request.Code);
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = product;
                return result;
            }
            catch (ProductNotValidExeption e)
            {

                e.ValidationErrors.ForEach(er =>
                {
                    result.AddError(ErrorCode.ValidationError, er);
                });
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            return result;
        }
    }
}
