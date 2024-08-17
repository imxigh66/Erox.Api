using Erox.Domain.Aggregates.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.WishlistAggregate
{
    public class WishlistItem
    {
        public Guid WishlistItemId { get;  set; }

        
        public Guid WishlistId { get;  set; }
        public Wishlist Wishlist { get; set; }

        
        public Guid ProductId { get;  set; }
        public Product Product { get;  set; }
    }
}
