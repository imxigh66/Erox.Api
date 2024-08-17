using Erox.Domain.Aggregates.UsersProfiles;
using Erox.Domain.Aggregates.WishlistAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erox.Domain.Aggregates.CardAggregate
{
    public class Card
    {
        public Guid CardId { get; set; }

        // Связь с пользователем
        public Guid UserId { get; set; }
        public UserProfileEntity User { get; set; }

        // Продукты в вишлисте
        public ICollection<CardItem> Items { get; set; } = new List<CardItem>();
    }
}
