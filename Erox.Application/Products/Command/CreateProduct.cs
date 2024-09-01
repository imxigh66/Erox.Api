using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Products.Command
{
    public  class CreateProduct:IRequest<OperationResult<Product>>
    {
        public ProductTranslationCreateCommand[] Names { get; set; }


        public ProductTranslationCreateCommand[] Descriptions { get; set; }


      
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        
        public Guid CategoryId { get; set; }
     
       
        public string[] Images { get; set; }
       
        public string Season { get; set; }
    
        public string Code { get; set; }
    }
}
