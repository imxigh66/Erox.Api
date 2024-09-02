using Erox.Domain.Aggregates.ProductAggregate;
using Erox.Domain.Aggregates.WishlistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.CardAggregate
{
    public class CardItem
    {
        public Guid CardItemId { get; set; }


        public Guid CardId { get; set; }
        public Card Card { get; set; }


        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ProductSize Size { get; set; }
        public Guid SizeId { get; set; }
        public int Quantity { get; set; }
    }
}
