namespace Erox.Api.Contracts.product.responses
{
    public class ProductSizeResponse
    {
        public Guid SizeId { get; set; }
        public Guid ProductId { get; set; }
        public string Size { get; set; }
    }
}
