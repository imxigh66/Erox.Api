using System.ComponentModel.DataAnnotations;

namespace Erox.Api.Contracts.wishlist.response
{
    public class AddWishlistResponse
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
