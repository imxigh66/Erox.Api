using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.product.requests
{
    public class ProductReviewCreate
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Rating { get;  set; }
        public bool IsApproved { get; set; }

        public ProductReviewCreate(string text, string rating, bool isApproved)
        {
            Text = text;
            Rating = rating;
            IsApproved = isApproved;
        }
    }
}
