using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.ProductAggregate
{
    public class ProductImages
    {
        public Guid Id { get; set; }
        public Guid ProductId {  get; set; }
        public Product Product { get; set; }
        public string Path {  get; set; }
    }
}
