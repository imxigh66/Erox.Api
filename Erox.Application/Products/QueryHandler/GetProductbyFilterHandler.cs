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
    public class GetProductbyFilterHandler : IRequestHandler<GetProductByFilter, OperationResult<Product[]>>
    {
        private readonly DataContext _ctx;
        public GetProductbyFilterHandler(DataContext ctx)
        {
                _ctx = ctx;
        }

       

        public async Task<OperationResult<Product[]>> Handle(GetProductByFilter request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product[]>();
            // var product = await _ctx.Products
            //.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);

            // if (product is null)
            // {

            //     result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound));
            //     return result;
            // }

            // result.PayLoad = product;
            // return result;
            try
            {
                // Фильтрация заказа по переданным параметрам
                var query =   _ctx.Products.AsQueryable();

                // Фильтр по UserId
                if (request.Category != null)
                {
                    query = query.Where(o => o.Category == request.Category);
                }

                // Фильтр по OrderId
                if (request.Color != null)
                {
                    query = query.Where(o => o.Color== request.Color);
                }

                

                // Фильтр по дате создания
                if (request.Season != null)
                {
                    query = query.Where(o => o.Season == request.Season);
                }
                if (request.Code != null)
                {
                    query = query.Where(o => o.Code == request.Code);
                }
                
                if (request.Price != null)
                {
                    query = query.Where(o => o.Price == request.Price);
                }

                // Получение заказов
                var products = await query.ToArrayAsync(cancellationToken);
                if (products is null || products.Length == 0)
                {

                    result.AddError(ErrorCode.NotFound, string.Format(ProductsErrorMessage.ProductNotFound));
                    return result;
                }



                result.PayLoad = products;
                return result;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
                return result;
            }

        }
    }
}
