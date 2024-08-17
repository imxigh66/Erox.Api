using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.cards.response
{
    public class CardProductResponse
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
