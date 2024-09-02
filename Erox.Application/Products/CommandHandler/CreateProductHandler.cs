using Erox.Application.Models;
using Erox.Application.Products.Command;
using Erox.DataAccess;

using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.Translations;
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
                var product = Product.CreateProduct(request.Price,request.DiscountPrice, request.CategoryId,request.Season, request.Code);

                product.ProductId = Guid.NewGuid();

                product.ProductNameTranslations = request.Names.Select(s => new ProductNameTranslation {
                    Id = Guid.NewGuid(),
                    Language = s.LanguageCode.ToString(),
                    ProductId = product.ProductId,
                    Title = s.Title,
                }).ToArray();
                
                product.ProductDescriptionTranslations = request.Descriptions.Select(s => new ProductDescriptionTranslation {
                    Id = Guid.NewGuid(),
                    Language = s.LanguageCode.ToString(),
                    ProductId = product.ProductId,
                    Title = s.Title,
                }).ToArray();


                _ctx.Products.Add(product);

                // Отладочная информация
                Console.WriteLine("Saving product to the database...");
                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = product;

                Console.WriteLine("Product saved successfully.");
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
