using Erox.Application.Enums;
using Erox.Application.Models;
using Erox.Application.Posts;
using Erox.Application.Products.Queries;
using Erox.DataAccess;
using Erox.Domain.Aggregates.PostAggregate;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.QueryHandler
{
    public class GetProductbyIdHandler : IRequestHandler<GetProductById, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        public GetProductbyIdHandler(DataContext ctx)
        {
                _ctx = ctx;
        }

       

        public async Task<OperationResult<Product>> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();
            var product = await _ctx.Products
           .FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

            if (product is null)
            {

                result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound));
                return result;
            }

            result.PayLoad = product;
            return result;
        }
    }
}
