using Erox.Application.Models;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Wishlists.Command
{
    public class AddToWishlist:IRequest<OperationResult<Wishlist>>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
