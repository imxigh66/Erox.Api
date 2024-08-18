using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.UsersProfiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

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

        public void UpdateOrder(string status, string paymenentMethod, string shippingMethod, decimal sum, string address, ICollection<OrderItem> items)
        {
            Status = status;
            PaymenentMethod = paymenentMethod;
            ShippingMethod=shippingMethod;
            Sum = sum;
            Address = address;
            Items= items;


            LastModified = DateTime.UtcNow;
        }
    }
}
