using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.product.requests
{
    public class ProductCreate
    {
        [Required]
       public ProductTranslationRequest[] Names { get; set; }

        [Required]
        public ProductTranslationRequest[] Descriptions { get; set; }

      
        [Required]
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Season { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
