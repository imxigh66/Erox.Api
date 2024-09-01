using Erox.Api.Contracts.product.responses;
using Erox.Domain.Aggregates.ProductAggregate;

namespace Erox.Api.Contracts.cards.response
{
    public class CardProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductSizeResponse[] Sizes { get; set; }
    }
}
