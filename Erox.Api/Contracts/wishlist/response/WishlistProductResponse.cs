using Erox.Api.Contracts.product.responses;

namespace Erox.Api.Contracts.wishlist.response
{
    public class WishlistProductResponse
    {
        
        public Guid ProductId { get; set; }
        public ProductTranslationResponse[] ProductName { get; set; }
        public string[] Image {  get; set; }  
        public decimal Price { get; set; }
    }
}
