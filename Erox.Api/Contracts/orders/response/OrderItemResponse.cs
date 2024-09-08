using Erox.Api.Contracts.product.responses;

namespace Erox.Api.Contracts.orders.response
{
    public class OrderItemResponse
    {
        public Guid OrderItemId { get; set; }

        public Guid OrderId { get; set; }

        public ProductTranslationResponse[] Names { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    
        public string? Code { get; set; }

        public Guid ProductId { get; set; }
        public string[] Images { get; set; }
   
        public ProductSizeResponse Size { get; set; }
        public int Quantity { get; set; }
    }
}
