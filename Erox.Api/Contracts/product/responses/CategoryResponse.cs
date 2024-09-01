namespace Erox.Api.Contracts.product.responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Sex { get; set; }
        public ProductTranslationResponse[] Names { get; set; }

    }
}
