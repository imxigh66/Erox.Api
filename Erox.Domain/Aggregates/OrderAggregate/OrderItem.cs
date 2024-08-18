using Erox.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.OrderAggregate
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ProductSize Size { get; set; }
        public Guid SizeId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}
