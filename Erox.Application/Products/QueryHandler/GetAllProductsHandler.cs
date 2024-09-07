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
    public class GetAllProductsHandler : IRequestHandler<GetAllProducts, OperationResult<List<Product>>>
    {
        private readonly DataContext _ctx;
        public GetAllProductsHandler(DataContext ctx)
        {
                _ctx = ctx;
        }
        public async Task<OperationResult<List<Product>>> Handle(GetAllProducts request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Product>>();
            try
            {
                var product = await _ctx.Products.Include(i => i.Sizes)
                    .Include(i => i.Category).ThenInclude(i => i.CategoryTranslations)
                    .Include(i => i.ProductNameTranslations)
                    .Include(i => i.ProductDescriptionTranslations)
                    .Include(i => i.Images).ToListAsync();


                result.PayLoad = product;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);

            }
            return result;
        }
    }
}
