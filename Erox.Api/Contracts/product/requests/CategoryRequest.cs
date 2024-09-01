using Erox.Domain.Enumerations;

namespace Erox.Api.Contracts.product.requests
{
    public class CategoryRequest
    {
        public SexEnum Sex { get; set; }
        public ProductTranslationRequest[] CategoryTranslation { get; set; }   
    }
}
