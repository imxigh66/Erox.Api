using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Products.Command;
using Erox.DataAccess;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.CommandHandler
{
    public class UploadProductImagesHandler : IRequestHandler<UploadProductImagesCommand, OperationResult<List<string>>>
    {
        private readonly DataContext _ctx;

        public UploadProductImagesHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<string>>> Handle(UploadProductImagesCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<string>>();
            var imagePaths = new List<string>();

            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId, cancellationToken);

                if (product == null)
                {
                    result.AddError(ErrorCode.NotFound, "Product not found");
                    return result;
                }

                foreach (var file in request.Files)
                {
                    var imagePath = await UploadAndSaveImageAsync(file);
                    imagePaths.Add(imagePath);

             

                    var productImage = new ProductImages
                    {
                        Id = Guid.NewGuid(),
                        Path = imagePath,
                        ProductId = product.ProductId
                    };

                    _ctx.ProductImages.Add(productImage);
                }

                await _ctx.SaveChangesAsync(cancellationToken);
                result.PayLoad = imagePaths;
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;
                result.AddUnknownError(innerExceptionMessage);
            }

            return result;
        }

        private async Task<string> UploadAndSaveImageAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/images/products/{fileName}";
        }
    }
    }
