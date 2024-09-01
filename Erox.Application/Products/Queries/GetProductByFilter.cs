using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Queries
{
    public class GetProductByFilter : IRequest<OperationResult<Product[]>>
    {
       public Guid? ProductId { get; set; }
       public Guid? CategoryId {  get; set; }
        public string? Season { get; set; }
        public string? Code { get; set; }
        public decimal? Price { get; set; }
       
    }
}
