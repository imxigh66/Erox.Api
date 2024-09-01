using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.DataAccess.Migrations;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.Translations;
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
                var product = await _ctx.Products
                    .Include(p => p.ProductNameTranslations)
                    .Include(p => p.ProductDescriptionTranslations)
                    .FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken: cancellationToken);

                if (product is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound, request.ProductId));
                    return result;
                }

                // Update product details
                product.UpdateProducts(request.Price, request.DiscountPrice, request.CategoryId,  request.Season, request.Code);

                // Update translations
                var existingNameTranslations = product.ProductNameTranslations.ToList();
                foreach (var translation in request.Names)
                {
                    var existingTranslation = existingNameTranslations.FirstOrDefault(t => t.Language == translation.LanguageCode.ToString());
                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = translation.Title;
                    }
                    else
                    {
                        product.ProductNameTranslations = product.ProductNameTranslations.Concat(new[]
                        {
                            new ProductNameTranslation
                            {
                                Id = Guid.NewGuid(),
                                Language = translation.LanguageCode.ToString(),
                                ProductId = product.ProductId,
                                Title = translation.Title,
                            }
                        }).ToArray();
                    }
                }
                var existingDescriptionTranslations = product.ProductDescriptionTranslations.ToList();
                foreach (var translation in request.Descriptions)
                {
                    var existingTranslation = existingDescriptionTranslations.FirstOrDefault(t => t.Language == translation.LanguageCode.ToString());
                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = translation.Title;
                    }
                    else
                    {
                        product.ProductDescriptionTranslations = product.ProductDescriptionTranslations.Concat(new[]
                        {
                            new ProductDescriptionTranslation
                            {
                                Id = Guid.NewGuid(),
                                Language = translation.LanguageCode.ToString(),
                                ProductId = product.ProductId,
                                Title = translation.Title,
                            }
                        }).ToArray();
                    }
                }


                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = product;
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
