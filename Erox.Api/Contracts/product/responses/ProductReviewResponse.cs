namespace Erox.Api.Contracts.product.responses
{
    public class ProductReviewResponse
    {
        public Guid ReviewId { get; set; }
        public string Text { get; set; }
     
        public string Rating { get; set; }
        public bool IsApproved { get; set; }
    }
}
