using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public class UpdateProduct:IRequest<OperationResult<Product>>
    {
        public Guid ProductId { get; set; }
        public ProductTranslationCreateCommand[] Names { get; set; }


        public ProductTranslationCreateCommand[] Descriptions { get; set; }



        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }


        public string Image { get; set; }

        public string Season { get; set; }

        public string Code { get; set; }
    }
}
