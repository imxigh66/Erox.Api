using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.product.requests
{
    public class ProductReviewCreate
    {
        [Required]
        public string Text { get; set; }
       
        public bool IsApproved { get; set; }

        public ProductReviewCreate(string text, bool isApproved)
        {
            Text = text;
           
            IsApproved = isApproved;
        }
    }
}
