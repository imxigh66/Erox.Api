using Erox.Api.Contracts.product.requests;

namespace Erox.Api.Contracts.product.responses
{
    public class ProductResponce
    {
        public ProductResponce()
        {
                
        }
        public Guid ProductId { get; set; }

        public ProductTranslationResponse[] Names { get; set; }
        public ProductTranslationResponse[] Descriptions { get;  set; }
        public decimal Price { get;     set; }
        public decimal DiscountPrice { get;     set; }  
        public CategoryResponse Category { get; set; }
        public ProductSizeResponse[] Sizes { get; set; }
        public string Color { get;     set; }
        public string Image { get; set; }
        public string Season { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}
