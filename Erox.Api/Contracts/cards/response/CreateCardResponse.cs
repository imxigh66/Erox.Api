using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.cards.response
{
    public class CreateCardResponse
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
