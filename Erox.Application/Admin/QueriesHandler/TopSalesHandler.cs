using Erox.DataAccess;
using Erox.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Admin.QueriesHandler
{
    public class TopSalesHandler
    {
        private readonly DataContext _ctx;
        public TopSalesHandler(DataContext ctx)
        {
                _ctx = ctx;
        }

        public async Task<List<ProductSalesDto>> GetTopSellingProductsAsync(int topCount = 10)
        {
            var topProducts = await _ctx.Set<OrderItem>()
    .Include(oi => oi.Product)  // Подключаем продукт для получения информации
    .ThenInclude(p => p.ProductNameTranslations)
    .Include(oi=>oi.Product).ThenInclude(i=>i.Images)// Подключаем переводы
    .ToListAsync(); // Загружаем в память

            // Группируем и обрабатываем данные в памяти
            var result = topProducts
                .GroupBy(oi => oi.ProductId)
                .Select(group => new ProductSalesDto
                {
                    ProductId = group.Key,
                    Code=group.First().Product.Code,
                    ProductName = group.First().Product.ProductNameTranslations
                                    .FirstOrDefault(t => t.Language == "en")?.Title ?? "No translation",
                    Images = group.First().Product.Images
                 .Select(t => t.Path)
                 .FirstOrDefault() ?? "default-image-path",

                    TotalQuantitySold = group.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(p => p.TotalQuantitySold)
                .Take(topCount)
                .ToList();

            return result;

        }
    }

    // DTO для возврата данных о продукте и количестве продаж
    public class ProductSalesDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Code { get; set; }
        public string Images {  get; set; }
        public int TotalQuantitySold { get; set; }
    }

}
