using Erox.Application.Models;
using Erox.Domain.Aggregates.ProductAggregate;
using MediatR;
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
        public string Name { get; set; }

       
        public string Description { get; set; }


      
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        
        public string Category { get; set; }
     
        public string Size { get; set; }
     
        public string Color { get; set; }
       
        public string Image { get; set; }
       
        public string Season { get; set; }
    
        public string Code { get; set; }
    }
}
