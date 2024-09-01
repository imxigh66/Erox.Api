using Erox.Domain.Enumerations;

namespace Erox.Api.Contracts.product.responses
{
    public class ProductTranslationResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public LanguageCodeEnum LanguageCode { get; set; }
    }
}
