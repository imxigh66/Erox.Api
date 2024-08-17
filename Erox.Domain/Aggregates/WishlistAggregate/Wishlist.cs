using Erox.Domain.Aggregates.UsersProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.WishlistAggregate
{
    public class Wishlist
    {
        public Guid WishlistId { get;  set; }

        // Связь с пользователем
        public Guid UserId { get;  set; }
        public UserProfileEntity User { get;  set; }

        // Продукты в вишлисте
        public ICollection<WishlistItem> Items { get;  set; } = new List<WishlistItem>();
    }
}
