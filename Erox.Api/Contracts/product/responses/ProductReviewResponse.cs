namespace Erox.Api.Contracts.product.responses
{
    public class ProductReviewResponse
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public ProductTranslationResponse[] Names { get; set; }
        public string Code { get; set; }
        public Guid ReviewId { get; set; }
        public string Text { get; set; }
        public bool IsApproved { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
