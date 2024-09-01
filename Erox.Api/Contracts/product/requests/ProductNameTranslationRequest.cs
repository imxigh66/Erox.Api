using Erox.Domain.Enumerations;

namespace Erox.Api.Contracts.product.requests
{
    public class ProductNameTranslationRequest
    {
        public string Title { get; set; }
        public LanguageCodeEnum LanguageCode { get; set; }
    }
}
