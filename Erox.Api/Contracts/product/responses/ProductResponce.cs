namespace Erox.Api.Contracts.product.responses
{
    public class ProductResponce
    {
        public Guid ProductId { get; private set; }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public decimal DiscountPrice { get; private set; }
        public string Category { get; private set; }
        public ProductSizeResponse[] Sizes { get; private set; }
        public string Color { get; private set; }
        public string Image { get; private set; }
        public string Season { get; private set; }
        public string Code { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime LastModified { get; private set; }
    }
}
