using Erox.Domain.Aggregates.ProductAggregate;

namespace Erox.Api.Contracts.cards.response
{
    public class CardProductResponse
    {
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public ProductSize Size { get; set; }   
    }
}
