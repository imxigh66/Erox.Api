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
    public class GetProductById:IRequest<OperationResult<Product>>
    {
        public Guid ProductId { get; set; }
    }
}
