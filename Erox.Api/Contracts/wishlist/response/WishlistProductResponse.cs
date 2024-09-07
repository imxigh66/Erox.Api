using Erox.Api.Contracts.product.responses;

namespace Erox.Api.Contracts.wishlist.response
{
    public class WishlistProductResponse
    {
        
        public Guid ProductId { get; set; }
        public ProductTranslationResponse[] Names { get; set; }
        public string[] Images {  get; set; }  
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
