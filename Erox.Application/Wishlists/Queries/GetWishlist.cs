using Erox.Application.Models;
using Erox.Domain.Aggregates.WishlistAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Application.Wishlists.Queries
{
    public class GetWishlist:IRequest<OperationResult<List<WishlistItem>>>
    {
        public Guid UserId { get; set; }
    }
}
