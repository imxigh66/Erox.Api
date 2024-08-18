using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.OrderAggregate
{
    public class Order
    {
        public Guid OrderId { get; set; }

        
        public Guid UserId { get; set; }
        public UserProfileEntity User { get; set; }
        public string Status { get; set; }
        public string PaymenentMethod { get; set; }
        public string ShippingMethod { get; set; }
        public decimal Sum { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get;  set; }
        public DateTime LastModified { get;  set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
